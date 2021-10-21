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
    public class ConferentionsController : Controller
    {
        private readonly Context _context;

        public ConferentionsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var conferentions = await _context.ConferentionsMembers
                .Where(m => m.PhoneNumber.Id == CurrentPhoneUserService.CurrentPhoneUser.Id)
                .Select(m => m.Conferention)
                .ToListAsync();

            return View(conferentions);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Conferention conferention)
        {
            if (ModelState.IsValid)
            {
                await _context.Conferentions.AddAsync(conferention);
                await _context.ConferentionsMembers.AddAsync(new ConferentionMember { Conferention = conferention, PhoneNumber = CurrentPhoneUserService.CurrentPhoneUser });
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

        public async Task<IActionResult> Delete(Guid id)
        {
            var conferention = await _context.Conferentions.FindAsync(id);

            if (conferention == null)
            {
                return NotFound();
            }

            return View(conferention);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var conferention = await _context.Conferentions.FindAsync(id);

            if (conferention == null)
            {
                return NotFound();
            }

            _context.Conferentions.Remove(conferention);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var conferention = await _context.Conferentions.FindAsync(id);

            if(conferention == null)
            {
                return NotFound();
            }

            return View(await ToViewModel(conferention));
        }

        private async Task<ConferentionViewModel> ToViewModel(Conferention conferention)
        {
            var list = await _context.ConferentionsMembers
                .Where(x => x.Conferention.Id == conferention.Id)
                .ToListAsync();

            foreach (var item in list)
            {
                item.PhoneNumber = await _context.PhoneNumbers.FindAsync(item.PhoneNumber.Id);
            }

            return new ConferentionViewModel
            {
                Conferention = conferention,
                MembersPhoneNumbers = list.Select(x => x.PhoneNumber.PhoneNum)
            };
        }

    }
}
