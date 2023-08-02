using Microsoft.AspNetCore.Mvc;

namespace _3gim.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Print()
        {
            return View();
        }

        public IActionResult Count()
        {
            return View();
        }
    }
}
