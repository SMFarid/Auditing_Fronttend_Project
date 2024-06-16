using Microsoft.AspNetCore.Mvc;

namespace Frontend_Project.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet] 
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
    }
}
