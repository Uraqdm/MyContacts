using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
using System;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class PhoneNumbersController : Controller
    {
        private readonly Context _context;

        public PhoneNumbersController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.PhoneNumbers.ToListAsync());

        public IActionResult AddNewNumber() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewNumber(PhoneNumber phone)
        {
            if (ModelState.IsValid)
            {
                await _context.PhoneNumbers.AddAsync(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(phone);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var phoneNum = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNum == null)
            {
                return NotFound();
            }

            return View(phoneNum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid phoneId, PhoneNumber phoneNum)
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

        public async Task<IActionResult> Delete(Guid id)
        {
            var phone = await _context.PhoneNumbers.FindAsync(id);

            if(phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var phoneNum = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNum == null)
            {
                return NotFound();
            }

            _context.PhoneNumbers.Remove(phoneNum);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Call(PhoneNumber phoneNum)
        {
            if (phoneNum == null)
            {
                return NotFound();
            }
            var call = new Call
            {
                Date = DateTime.Now,
                From = phoneNum
            };

            await _context.Calls.AddAsync(call);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
