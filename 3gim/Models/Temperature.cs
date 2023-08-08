using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    [PrimaryKey(nameof(Date), nameof(Time))]
    public class Temperature
    {
        
        public string Date { get; set; } // 날짜
        public string Time { get; set; } // 시간
        public int Tempe { get; set; } // 온도
    }
}
