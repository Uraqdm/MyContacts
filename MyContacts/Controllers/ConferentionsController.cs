using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.Models;
using System;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class ConferentionsController : Controller
    {
        private readonly Context _context;

        public ConferentionsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Conferentions.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conferention conferention)
        {
            if (ModelState.IsValid)
            {
                await _context.Conferentions.AddAsync(conferention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(conferention);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var conferention = await _context.Conferentions.FindAsync(id);

            if(conferention == null)
            {
                return NotFound();
            }

            return View(conferention);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Conferention conferention)
        {
            if (id != conferention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Conferentions.Update(conferention);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(conferention);
        }
    }
}
