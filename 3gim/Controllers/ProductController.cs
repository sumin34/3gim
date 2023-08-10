﻿using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [HttpGet("regist")]
        public IActionResult Regist()
        {
            var result = _dbContext.Product.OrderBy(product => product.ProductID).ToList();
            return View(result);
        }

        [HttpPost("regist")]
        public IActionResult Regist(Product product)
        {
            _dbContext.Add(product);

            _dbContext.SaveChanges();

            return View();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var result = _dbContext.Product.OrderBy(product => product.ProductID).ToList();

            return View(result);
        }

        [HttpGet("read/{productname}")]
        public string Read(string productname)
        {
            var result = _dbContext.Product.Where(product => product.ProductName == productname).FirstOrDefault();

            var json = new JObject();
            json.Add("ProductID", result.ProductID);
            json.Add("ProductName", result.ProductName);
            json.Add("ProductPrice", result.ProductPrice);
            json.Add("ProductExp", result.ProductExp);
            Console.WriteLine(json.ToString());

            return json.ToString();

        }



        [HttpPost("delete/{productname}")]
        public IActionResult Delete(string productname)
        {
            var result = _dbContext.Product.Where(product => product.ProductName == productname).FirstOrDefault();
            _dbContext.Remove(result);
            _dbContext.SaveChanges();

            return View();
        }


        [HttpGet("order")]
        public IActionResult Order()
        {
            var result = _dbContext.Product.Include(p => p.Release).ToList();
            return View(result);
        }

        [HttpPost("order")]
        public IActionResult Order(Release release)
        {
            var result = _dbContext.Release.Where(r => r.ProductID == release.ProductID).FirstOrDefault();
            _dbContext.Add(result);
            _dbContext.SaveChanges();
            return View();
        }

        [HttpGet("quantity")]
        public IActionResult Quantity()
        {
            var result = _dbContext.Product.Include(p => p.Store).ToList();
            return View(result);
        }

        [HttpGet("edit")]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost("edit")]
        public IActionResult Edit(Product product)
        {
            var result = _dbContext.Product.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            result.ProductName = product.ProductName;
            result.ProductPrice = product.ProductPrice;
            result.ProductExp = product.ProductExp;

            _dbContext.SaveChanges();

            return View();
        }



        [HttpGet("listedit")]
        public IActionResult ListEdit()
        {
            return View();
        }


        [HttpPost("listedit")]
        public IActionResult ListEdit(Store store)
        {
            var result = _dbContext.Store.Where(s => s.ProductID == s.ProductID).FirstOrDefault();
            result.ProductionDate = store.ProductionDate;
            result.ProductQuantity = store.ProductQuantity;
            result.Remarks = store.Remarks;

            _dbContext.SaveChanges();

            return View();
        }




    }
}
