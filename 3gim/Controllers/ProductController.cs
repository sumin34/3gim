using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3gim.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly _3gimDbContext _dbContext;
        private readonly UserManager<_3gimMember> _userManager;


        public ProductController(_3gimDbContext dbContext, UserManager<_3gimMember> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet("print")]
        public IActionResult Print()
        {
            return View();
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return View();
        }

        [HttpGet("Order")]
        public IActionResult Order()
        {
            return View();
        }

        [HttpGet("Productquantity")]
        public IActionResult Productquantity()
        {
            return View();
        }

        [HttpGet("productedit")]
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
        public IActionResult Productregist(Product product)
        {
            _dbContext.Add(product);

            _dbContext.SaveChanges();

            return Redirect("/product/productregist");
        }

      
    }
}
