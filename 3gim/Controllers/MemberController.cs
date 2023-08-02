using Microsoft.AspNetCore.Mvc;

namespace _3gim.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
    }
}
