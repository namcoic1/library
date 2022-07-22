using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_Project.Models
{
    public partial class Borrow
    {
        public Borrow()
        {
            BorrowDetails = new HashSet<BorrowDetail>();
        }

        public int BrId { get; set; }
        public int? UId { get; set; }
        public DateTime? BrDate { get; set; }
        public DateTime? BrResend { get; set; }

        public Borrow(int? uId, DateTime? brDate, DateTime? brResend)
        {
            UId = uId;
            BrDate = brDate;
            BrResend = brResend;
        }

        public virtual User UIdNavigation { get; set; }
        public virtual ICollection<BorrowDetail> BorrowDetails { get; set; }
    }
}
