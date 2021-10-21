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
            var members = await _context.ConferentionsMembers.Where(x => x.ConferentionId == id).ToListAsync();

            foreach (var item in members)
            {
                item.PhoneNumber = await _context.PhoneNumbers.FindAsync(item.PhoneId);
            }

            conferentionId = id;

            return View(members);
        }

        public IActionResult AddMember() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(MemberViewModel memberVM)
        {
            if (ModelState.IsValid)
            {
                var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == memberVM.PhoneNum).FirstOrDefaultAsync();

                if(phone == null || _context.ConferentionsMembers.Where(m => m.PhoneId == phone.Id && m.ConferentionId == memberVM.ConferentionId).Any())
                {
                    return View(memberVM);
                }

                var record = new ConferentionMember
                {
                    ConferentionId = conferentionId,
                    PhoneId = phone.Id
                };
                await _context.ConferentionsMembers.AddAsync(record);
                await _context.SaveChangesAsync();

                return RedirectToAction(actionName: "Details", controllerName: "Conferentions", new { id = conferentionId });
            }

            return View(memberVM);
        }

        public async Task<IActionResult> RemoveMember(Guid id)
        {
            var member = await _context.ConferentionsMembers.FindAsync(id);

            if(member == null)
            {
                return NotFound();
            }

            member.PhoneNumber = await _context.PhoneNumbers.FindAsync(member.PhoneId);

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

            return RedirectToAction(nameof(Index), new { id = member.ConferentionId });
        }
    }
}
