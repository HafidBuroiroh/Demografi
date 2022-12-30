using Microsoft.AspNetCore.Mvc;
using Demografi.Models;
using Demografi.Services;

namespace Demografi.Controllers
{

 
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;

        public LoginController(IAccountService AccountService)
        {
            accountService = AccountService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Home")]
         public IActionResult Login(string username, string password)
        {
            var account = accountService.Login(username, password);
            if(account != null) 
            {
                return Redirect("Home");
            
            } else
            {
                ViewBag.msg = "Invalid";
                return View("Index");

            }
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }

   
}
