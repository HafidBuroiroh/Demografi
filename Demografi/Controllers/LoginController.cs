using Microsoft.AspNetCore.Mvc;
using Demografi.Models;
using Demografi.Services;

namespace Demografi.Controllers
{

    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;

        public LoginController(IAccountService AccountService)
        {
            accountService = AccountService;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            var account = accountService.Login(username, password);
            if(account != null) 
            {
                return View("home");
            
            } else
            {
                ViewBag.msg = "Invalid";
                return View("Index");

            }
        }

        [Route("home")]
        public IActionResult Home()
        {
            return View("Home");
        }



        [Route("logout")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }

   
}
