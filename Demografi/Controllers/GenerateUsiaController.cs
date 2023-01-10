using Demografi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateUsiaController : Controller
    {
        private readonly DataContext _context;
        public GenerateUsiaController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_usia_saved.ToList());
        }
    }
}
