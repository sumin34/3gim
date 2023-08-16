using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
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
            var result = _dbContext.Order.Include(p => p.PID).ToList();
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
        public async Task<IActionResult> Detail()
        {
            var result = await _dbContext.Warehousing
                        .Include(p => p.PID)
                        .Select(w => w.PID.ProductName) // Product 모델의 상품 이름 선택
                        .ToListAsync();

            var productname = await _dbContext.Product
                        .Select(w => w.ProductName) // Product 모델의 상품 이름 선택
                        .ToListAsync();

            ViewBag.ProductNames = result;

            return View(productname);

        }


        [HttpPost("detail")]
        public IActionResult Detail(Warehousing ware)
        {
            int productId = ware.ProductID;
            _dbContext.Add(ware);
            _dbContext.SaveChanges();

            return RedirectToAction("Detail");
        }


        [HttpGet("detailajax")]
        public String DetailAjax(String productName)
        {
            var result = _dbContext.Warehousing
                        .Include(w => w.PID)
                        .Where(w => w.PID.ProductName == productName)
                        .OrderByDescending(w => w.Id)
                        .ToList();

            JArray jArray = new JArray();

            for (int i = 0; i < result.Count; i++)
            {
                JObject jObject = new JObject(
                    new JProperty("상품번호", result[i].ProductID),
                    new JProperty("제조번호", result[i].Id),
                    new JProperty("날짜", result[i].Date),
                    new JProperty("상품이름", result[i].PID.ProductName),
                    new JProperty("입고개수", result[i].Store),
                    new JProperty("출고개수", result[i].Release),
                    new JProperty("비고", result[i].Note)
                    );
                jArray.Add(jObject);
            }

            Console.WriteLine(jArray.ToString());
            return jArray.ToString();
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


        [HttpGet("Supplies")]
        public int Supplies()
        {
            int quantity = 0;
            var result = _dbContext.Supplies.ToList();
            if(result.Count == 0)
            {
                return quantity;
            }
            else
            {
                var result2 = _dbContext.Supplies.First();
                quantity = result2.Quantity;
                return quantity;
            }
           
        }

        [HttpGet("Defects")]
        public int Defects()
        {
            int quantity = 0;
            var result = _dbContext.Defects.ToList();
            if (result.Count == 0)
            {
                return quantity;
            }
            else
            {
                var result2 = _dbContext.Defects.First();
                quantity = result2.Quantity;
                return quantity;
            }

        }
    }
}
