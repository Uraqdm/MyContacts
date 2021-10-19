using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.DatabaseLayer;
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
    }
}
