using Microsoft.AspNetCore.Mvc;

namespace _3gim.Controllers
{
    [Route("product")]
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
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Productquantity()
        {
            return View();
        }

        public IActionResult Productedit()
        {
            return View();
        }

        [HttpGet("productregist")]
         public IActionResult Productregist()
        {
            return View();
        }

        [HttpPost("productregist")]
        public IActionResult Productregist(string productname, int exp)
        {
            return View();
        }

      
    }
}
