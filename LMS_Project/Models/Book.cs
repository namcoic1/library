using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_Project.Models
{
    public partial class Book
    {
        public Book()
        {
            BorrowDetails = new HashSet<BorrowDetail>();
        }

        public Book(int bId, string bName, int? bStock, decimal? bPrice, string bDesc, string bCateId)
        {
            BId = bId;
            BName = bName;
            BStock = bStock;
            BPrice = bPrice;
            BDesc = bDesc;
            BCateId = bCateId;
        }

        public int BId { get; set; }
        public string BName { get; set; }
        public string BImg { get; set; }
        public int? BStock { get; set; }
        public decimal? BPrice { get; set; }
        public string BDesc { get; set; }
        public string BCateId { get; set; }
        public int? UId { get; set; }
        public bool? BStatus { get; set; }
        public DateTime? BLastupdated { get; set; }
        public int? BNumBorrow { get; set; }

        public virtual BookCategory BCate { get; set; }
        public virtual User UIdNavigation { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetails { get; set; }
    }
}
