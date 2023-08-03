using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    [PrimaryKey(nameof(Date), nameof(Time))]
    public class TemperatureHumidity
    {
        
        public string Date { get; set; } // 날짜
        public string Time { get; set; } // 시간
        public int Temperature { get; set; } // 온도
        public int Humidity { get; set;} // 습도
    }
}
