using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{

    public class Release
    {

        [Key]
        public int OrderNumber { get; set; } // 주문번호
        public string Vendor { get; set; } // 거래처
        public int OrderQuantity { get; set; } // 주문수량
        public bool ReleaseCheck { get; set; } // 출고여부
        public string Remarks { get; set; } // 기타
        public int ProductID { get; set; }
        public Product PID { get; set; }

    }
}
