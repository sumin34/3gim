﻿using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } // 상품번호
        public string ProductName { get; set; } // 상품명
        public int ProductPrice { get; set; } // 상품 가격
        public int ProductExp { get; set; } // 상품 유통기한
        

        public List<Warehousing> Warehousing { get; set; } = new List<Warehousing>();
        public List<Order> Order { get; set; } = new List<Order>();
    }
}
