using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{

    [PrimaryKey(nameof(ProductionDate), nameof(ProductID))]
    public class Store
    {

        public int ProductQuantity { get; set; } // 상품 수량
        
        public string ProductionDate { get; set; } // 상품 생산 날짜
        public string Remarks { get; set; } // 기타

        public int ProductID { get; set; }
        public Product PID { get; set; }
    }
}
