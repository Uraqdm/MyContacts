using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
using System;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class PhonesController : Controller
    {
        private readonly Context _context;

        public PhonesController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.PhoneNumbers.ToListAsync());

        public IActionResult AddNewNumber() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewNumber([Bind("Id, PhoneNumber")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                await _context.PhoneNumbers.AddAsync(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(phone);
        }

        public async Task<IActionResult> Edit(Guid phoneId)
        {
            var phoneNum = await _context.PhoneNumbers.FindAsync(phoneId);

            if (phoneNum == null)
            {
                return NotFound();
            }

            return View(phoneNum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid phoneId, [Bind("Id, PhoneNumber")] Contact phoneNum)
        {
            if (phoneId != phoneNum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(phoneNum);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(phoneNum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContact(Guid phoneId)
        {
            var phoneNum = await _context.PhoneNumbers.FindAsync(phoneId);

            if (phoneNum == null)
            {
                return NotFound();
            }

            _context.PhoneNumbers.Remove(phoneNum);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
