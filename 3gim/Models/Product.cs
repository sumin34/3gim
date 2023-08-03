using System.ComponentModel.DataAnnotations;

namespace _3gim.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } // 상품번호
        public string ProductName { get; set; } // 상품명
        public int ProductExp { get; set; } // 상품 유통기한

        public List<Store> Store { get; set; } = new List<Store>();

        public List<Release> Release { get; set; } = new List<Release>();
    }
}
