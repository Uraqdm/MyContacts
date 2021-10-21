using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
using MyContacts.Services;
using MyContacts.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly Context _context;

        public ContactsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
                .Where(c => c.Owner.Id == CurrentPhoneUserService.CurrentPhoneUser.Id)
                .Include(p => p.PhoneNumber)
                .ToListAsync();

            return View(contacts);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        public IActionResult CreateContact()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact(ContactViewModel contactVM)
        {
            var phoneNum = await FindPhoneAsync(contactVM.PhoneNum);

            if (ModelState.IsValid && phoneNum != null && !IsPhoneNumUses(phoneNum.Id))
            {   
                var contact = contactVM.Contact;
                contact.PhoneNumber = phoneNum;
                contact.Owner = await _context.PhoneNumbers.FindAsync(CurrentPhoneUserService.CurrentPhoneUser.Id);

                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contactVM);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(new ContactViewModel { Contact = contact, PhoneNum = contact.PhoneNumber.PhoneNum });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactViewModel contactVM)
        {
            var phone = await FindPhoneAsync(contactVM.PhoneNum);

            if(id != contactVM.Contact.Id && phone == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid && !IsPhoneNumUses(phone.Id))
            {
                var contact = contactVM.Contact;
                contact.PhoneNumber = phone;
                _context.Update(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(contactVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<PhoneNumber> FindPhoneAsync(string phoneNum)
        {
            var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == phoneNum).FirstOrDefaultAsync();

            return phone;
        }

        private bool IsPhoneNumUses(Guid id)
        {
            return _context.Contacts.Where(c => c.PhoneNumber.Id == id && c.Owner.Id == CurrentPhoneUserService.CurrentPhoneUser.Id).Any();
        }
    }
}
