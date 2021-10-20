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
    public class CallsController : Controller
    {
        private readonly Context _context;

        public CallsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var calls = await _context.Calls.ToListAsync();

            foreach (var item in calls)
            {
                item.PhoneNumber = await _context.PhoneNumbers.FindAsync(item.PhoneNumberId);
            }

            return View(calls);
        }

        public async Task<IActionResult> Create(Guid id = default)
        {
            if(id != default)
            {
                //Если передали id, значит номер телефона точно существует, т.к. передать могли только с PhoneNumbers или Contacts контроллеров
                var phone = await _context.PhoneNumbers.FindAsync(id);
                var callVM = new CallViewModel { PhoneNum = phone.PhoneNum };
                return View(callVM);
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CallViewModel callVM)
        {
            var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == callVM.PhoneNum).FirstOrDefaultAsync();
            if (ModelState.IsValid && phone != null)
            {
                var call = new Call
                {
                    Date = DateTime.Now,
                    PhoneNumber = phone
                };

                await _context.Calls.AddAsync(call);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(callVM);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var call = await _context.Calls.FindAsync(id);

            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var call = await _context.Calls.FindAsync(id);

            if (call == null)
            {
                return NotFound();
            }

            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
