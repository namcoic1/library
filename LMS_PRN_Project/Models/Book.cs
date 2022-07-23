using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_PRN_Project.Models
{
    public partial class Book
    {
        public Book()
        {
            BorrowDetails = new HashSet<BorrowDetail>();
        }

        public int BId { get; set; }
        public string BName { get; set; }
        public string BImg { get; set; }
        public int? BStock { get; set; }
        public decimal? BPrice { get; set; }
        public string BDesc { get; set; }
        public string BCateId { get; set; }
        public int? AutId { get; set; }
        public bool? BStatus { get; set; }
        public DateTime? BLastupdated { get; set; }
        public int? BNumBorrow { get; set; }

        public virtual Author Aut { get; set; }
        public virtual BookCategory BCate { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetails { get; set; }
    }
}
