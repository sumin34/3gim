using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    public class Order
    {
        [Key]
        public int OrderNumber { get; set; } // 주문번호
        public string Vendor { get; set; } // 거래처
        public string ShippingAddress { get; set; }// 배송지
        public int ProductID { get; set; }
        public Product PID { get; set; }
        public int OrderQuantity { get; set; } // 주문수량
        public string Note { get; set; } // 비고
    }
}
