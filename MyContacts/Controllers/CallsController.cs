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

        public async Task<IActionResult> Index() => View(await _context.Calls.ToListAsync());

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
                    To = phone
                };

                await _context.Calls.AddAsync(call);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(callVM);
        }
    }
}
