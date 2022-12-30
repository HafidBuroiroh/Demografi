using Demografi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateController : Controller
    {
        private readonly DataContext _context;
        public GenerateController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_general_saved.ToList());
        }
    }
}
