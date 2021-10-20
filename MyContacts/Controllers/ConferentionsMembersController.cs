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

        public ConferentionsMembersController(Context context)
        {
            _context = context;
        }

        public IActionResult AddMember(Guid id) => View(new MemberViewModel { ConferentionId = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(Guid id, MemberViewModel memberVM)
        {
            if (ModelState.IsValid)
            {
                var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == memberVM.PhoneNum).FirstOrDefaultAsync();

                if (phone == null)
                {
                    return NotFound();
                }

                var record = new ConferentionMember
                {
                    ConferentionId = id,
                    PhoneId = phone.Id
                };
                await _context.ConferentionsMembers.AddAsync(record);
                await _context.SaveChangesAsync();

                return RedirectToAction(actionName: "Details", controllerName: "Conferentions", new { id = id });
            }

            return View(memberVM);
        }

        public IActionResult RemoveMember(string phoneNum) => View(phoneNum);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMemberSubmit(string phoneNum)
        {
            throw new Exception();
        }
    }
}
