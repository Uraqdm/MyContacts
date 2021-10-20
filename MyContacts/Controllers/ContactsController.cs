using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
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

        public async Task<IActionResult> Index() => View(await _context.Contacts.ToListAsync());

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
            var phoneNum = await _context.PhoneNumbers.Where(p => p.PhoneNum == contactVM.PhoneNum).FirstOrDefaultAsync();
          
            if (ModelState.IsValid && phoneNum != null)
            {   
                var contact = contactVM.Contact;
                contact.PhoneNumberId = phoneNum.Id;

                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contactVM);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid contactId, [Bind("Id, Name, MiddleName, LastName, PhoneNumber")] Contact contact)
        {
            if(contactId != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(contact);
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
    }
}
