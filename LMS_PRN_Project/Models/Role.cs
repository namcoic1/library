using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_PRN_Project.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RId { get; set; }
        public string RName { get; set; }
        public string RDesc { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
