using Demografi.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateProvinsiController : Controller
    {
        private readonly DataContext _context;
        public GenerateProvinsiController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_provinsi_saved);
        }
    }
}
