using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_Project.Models
{
    public partial class BorrowDetail
    {
        public int BId { get; set; }
        public int BrId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }

        public virtual Book BIdNavigation { get; set; }
        public virtual Borrow Br { get; set; }
    }
}
