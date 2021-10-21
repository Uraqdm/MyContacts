using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
using MyContacts.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MyContacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enter(PhoneNumberViewModel phoneNumber)
        {
            if (ModelState.IsValid)
            {
                var phone = await _context.PhoneNumbers.Where(p => p.PhoneNum == phoneNumber.PhoneNum).FirstOrDefaultAsync();

                if(phone == null)
                {
                    return View(phoneNumber);
                }

                return RedirectToAction(controllerName: "Contacts", actionName: "Index");
            }

            return View(phoneNumber);
        }
    }
}
