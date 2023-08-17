using _3gim.Data;
using _3gim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace _3gim.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly _3gimDbContext _dbContext;
        private readonly MySqlConnection _mySqlConnection;


        public HomeController(ILogger<HomeController> logger, _3gimDbContext dbContext, MySqlConnection mySqlConnection)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mySqlConnection = mySqlConnection;
        }

        public IActionResult Index()
        {
            _mySqlConnection.Close();
            _mySqlConnection.Open();
            string sql = "SELECT * FROM temperature ORDER BY time desc LIMIT 10";
            MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
            MySqlDataReader result = cmd.ExecuteReader();

            List<Temperature> temperatures = new List<Temperature>();

            while (result.Read())
            {
                Temperature temp = new Temperature();
                temp.Date = result.GetString("date");
                temp.Time = result.GetString("time");
                temp.Temp = result.GetFloat("temp");

                temperatures.Add(temp);
            }

            _mySqlConnection.Close();
            return View(temperatures);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Manager()
        {
            _mySqlConnection.Close();
            _mySqlConnection.Open();
            string sql = "SELECT * FROM temperature ORDER BY time desc LIMIT 10";
            MySqlCommand cmd = new MySqlCommand(sql, _mySqlConnection);
            MySqlDataReader result = cmd.ExecuteReader();

            List<Temperature> temperatures = new List<Temperature>();

            while (result.Read())
            {
                Temperature temp = new Temperature();
                temp.Date = result.GetString("date");
                temp.Time = result.GetString("time");
                temp.Temp = result.GetFloat("temp");

                temperatures.Add(temp);
            }

            _mySqlConnection.Close();

            return View(temperatures);
        }

    }
}

