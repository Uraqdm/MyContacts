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
    public class ConferentionsMembersController : Controller
    {
        private readonly Context _context;

        private static Guid conferentionId;

        public ConferentionsMembersController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var members = await _context.ConferentionsMembers
                .Where(x => x.Conferention.Id == id)
                .Include(x => x.PhoneNumber)
                .ToListAsync();


            conferentionId = id;

            return View(members);
        }

        public IActionResult AddMember() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(ConferentionMember member)
        {
            if (ModelState.IsValid)
            {
                var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == member.PhoneNumber.PhoneNum).FirstOrDefaultAsync();

                if(phone == null || await IsAlreadyMember(conferentionId, phone.Id))
                {
                    return View(member);
                }

                member.Conferention = await _context.Conferentions.FindAsync(conferentionId);
                member.PhoneNumber = phone;
                
                await _context.ConferentionsMembers.AddAsync(member);
                await _context.SaveChangesAsync();

                return RedirectToAction(actionName: "Details", controllerName: "Conferentions", new { id = conferentionId });
            }

            return View(member);
        }

        public async Task<IActionResult> RemoveMember(Guid id)
        {
            var member = await _context.ConferentionsMembers.FindAsync(id);

            if(member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMemberSubmit(Guid id)
        {
            var member = await _context.ConferentionsMembers.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.ConferentionsMembers.Remove(member);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id = conferentionId });
        }

        private async Task<bool> IsAlreadyMember(Guid conferentionId, Guid phoneId)
        {
            return await _context.ConferentionsMembers.Where(m => m.Conferention.Id == conferentionId && m.PhoneNumber.Id == phoneId).AnyAsync();


        }
    }
}
