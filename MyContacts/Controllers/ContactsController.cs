using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
using System;
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

        public async Task<IActionResult> Details(Guid contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        public IActionResult CreateContact() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContact([Bind("Id, Name, MiddleName, LastName, PhoneNumbers")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }

        public async Task<IActionResult> Edit(Guid contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);

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
        public async Task<IActionResult> DeleteContact(Guid contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);

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
