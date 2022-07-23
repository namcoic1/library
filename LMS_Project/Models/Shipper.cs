using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_Project.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            Borrows = new HashSet<Borrow>();
        }

        public int ShId { get; set; }
        public string ShName { get; set; }
        public bool? ShGender { get; set; }
        public string ShEmail { get; set; }
        public string ShPhone { get; set; }
        public string ShAddress { get; set; }
        public int? UId { get; set; }
        public bool? ShStatus { get; set; }

        public virtual User UIdNavigation { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
