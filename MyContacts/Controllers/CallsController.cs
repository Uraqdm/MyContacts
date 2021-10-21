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
    public class CallsController : Controller
    {
        private readonly Context _context;

        public CallsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var calls = await _context.Calls
                .Where(c => c.From.Id == CurrentPhoneUserService.CurrentPhoneUser.Id || c.To.Id == CurrentPhoneUserService.CurrentPhoneUser.Id)
                .Include(x => x.To)
                .Include(x => x.From)
                .ToListAsync();

            return View(calls);
        }

        public async Task<IActionResult> Create(Guid id = default)
        {
            if(id != default)
            {
                //Если передали id, значит номер телефона точно существует, т.к. передать могли только с PhoneNumbers или Contacts контроллеров
                var phone = await _context.PhoneNumbers.FindAsync(id);
                var call = new Call { To = phone };
                return View(call);
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Call call)
        {
            var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == call.To.PhoneNum).FirstOrDefaultAsync();
            
            if (ModelState.IsValid && phone != null && phone.Id != CurrentPhoneUserService.CurrentPhoneUser.Id)
            {
                var newCall = new Call
                {
                    Date = DateTime.Now,
                    From = await _context.PhoneNumbers.FindAsync(CurrentPhoneUserService.CurrentPhoneUser.Id),
                    To = phone
                };

                await _context.Calls.AddAsync(newCall);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(call);
        }
    }
}
