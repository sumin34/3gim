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

        [HttpGet("Regist")]
        public IActionResult Regist()
        {
            return View();
        }

        [HttpPost("Regist")]
        public IActionResult Regist(Product product)
        {
            _dbContext.Add(product);

            _dbContext.SaveChanges();

            return Redirect("/product/Regist");
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var result = _dbContext.Product.OrderBy(product => product.ProductID).ToList();

            return View(result);
        }

        [HttpGet("read/{productname}")]
        public IActionResult Read(string productname)
        {
            var result = _dbContext.Product.Where(product => product.ProductName == productname).FirstOrDefault();

            return View(result);
        }

        [HttpGet("delete/{productname}")]
        public IActionResult Delete(string productname)
        {
            var result = _dbContext.Product.Where(product => product.ProductName == productname).FirstOrDefault();
            _dbContext.Remove(result);
            _dbContext.SaveChanges();
            
            return View(result);
        }


        [HttpGet("Order")]
        public IActionResult Order()
        {
            return View();
        }

        [HttpGet("Quantity")]
        public IActionResult Quantity()
        {
            return View();
        }

        [HttpPost("Edit")]
        public IActionResult Edit(Product product)
        {
            var result = _dbContext.Product.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            result.ProductName = product.ProductName;
            result.ProductPrice = product.ProductPrice;
            result.ProductExp = product.ProductExp;

            _dbContext.SaveChanges();

            return View();
        }

        


    }
}
