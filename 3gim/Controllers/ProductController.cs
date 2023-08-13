using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using MySqlDataReader = MySql.Data.MySqlClient.MySqlDataReader;

namespace _3gim.Controllers
{
    [Route("product")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly _3gimDbContext _dbContext;
        private readonly UserManager<_3gimMember> _userManager;
        private readonly MySqlConnection _mySqlConnection;

        public ProductController(_3gimDbContext dbContext, UserManager<_3gimMember> userManager, MySqlConnection mySqlConnection)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mySqlConnection = mySqlConnection;
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

            return Redirect("/product/regist");
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
            var result = _dbContext.Order.Include(p=> p.PID).ToList();
            return View(result);
        }

 

        [HttpGet("quantity")]
        public IActionResult Quantity()
        {
            _mySqlConnection.Close();
            _mySqlConnection.Open();
            string sql = "select product.ProductID as '상품번호',product.productname as '상품명',sum(warehousing.Store) as '총입고',sum(warehousing.Release) as '총출고',sum(warehousing.Store)-sum(warehousing.Release) as '현재고' from warehousing join product where warehousing.ProductID=product.ProductID group by product.ProductID  order by product.ProductID";
            MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
            MySqlDataReader result = cmd.ExecuteReader();

            List<stock> stocks = new List<stock>();

            while (result.Read())
            {
                stock stock = new stock();
                //Console.WriteLine(result.GetInt32("상품번호"));
                //Console.WriteLine(result.GetString("상품명"));
                //Console.WriteLine(result.GetInt32("총입고"));
                //Console.WriteLine(result.GetInt32("총출고"));
                //Console.WriteLine(result.GetInt32("현재고"));

                stock.ProductID = result.GetInt32("상품번호");
                stock.ProductName = result.GetString("상품명");
                stock.TotalStore = result.GetInt32("총입고");
                stock.TotalRelease = result.GetInt32("총출고");
                stock.CurrentQuantity = result.GetInt32("현재고");


                stocks.Add(stock);
            }

            _mySqlConnection.Close();

            return View(stocks);
        }



        [HttpGet("detail")]
        public IActionResult Detail()
        {
            var result = _dbContext.Warehousing.Include(p => p.PID).ToList();


            Console.WriteLine(result);

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

            return Redirect("/product/edit");
        }


    }
}
