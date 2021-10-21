using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class CallsStatisticController : Controller
    {
        private readonly Context _context;

        public CallsStatisticController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> CalculateStatistic(Guid id)
        {
            var phoneNum = await _context.PhoneNumbers.FindAsync(id);

            return View(new StatisticViewModel { PhoneNumber = phoneNum.PhoneNum });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateStatistic(StatisticViewModel statisticVM)
        {
            if (ModelState.IsValid)
            {
                var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == statisticVM.PhoneNumber).FirstOrDefaultAsync();
                statisticVM.CallsCount = await _context.Calls
                    .Where(c => c.PhoneNumberId == phone.Id &&
                    c.Date <= statisticVM.EndDate && 
                    c.Date >= statisticVM.BeginDate)
                    .CountAsync();

                return RedirectToAction(nameof(Index), new { beging = statisticVM.BeginDate, end = statisticVM.EndDate, phone = statisticVM.PhoneNumber, count = statisticVM.CallsCount });
            }

            return View(statisticVM);
        }

        public IActionResult Index(DateTime begin, DateTime end, string phone, int count)
        {
            var statistic = new StatisticViewModel
            {
                BeginDate = begin,
                EndDate = end,
                PhoneNumber = phone,
                CallsCount = count
            };

            return View(statistic);
        }
    }
}
