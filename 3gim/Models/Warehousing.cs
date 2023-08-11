namespace _3gim.Models
{
    public class Warehousing
    {
        public int Id { get; set; } // No.
        public string Date { get; set; } // 날짜
        public int LotNumber { get; set; } // 제조번호
        public int Store { get; set; } // 입고 수량
        public int Release { get; set; } // 출고 수       
        public string Note { get; set; } // 기타
        public int ProductID { get; set; }
        public Product PID { get; set; }
    }
}
