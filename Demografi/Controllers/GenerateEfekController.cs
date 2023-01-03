using Demografi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateEfekController : Controller
    {
        private readonly DataContext _context;
        public GenerateEfekController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_tipe_efek_saved.ToList());
        }
    }
}
