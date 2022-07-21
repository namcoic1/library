using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_Project.Models
{
    public partial class User
    {
        public User()
        {
            Authors = new HashSet<Author>();
            Borrows = new HashSet<Borrow>();
        }

        public int UId { get; set; }
        public string UEmail { get; set; }
        public string UPassword { get; set; }
        public string UPhone { get; set; }
        public string UAddress { get; set; }
        public int? RId { get; set; }
        public string UWallet { get; set; }
        public string UUsername { get; set; }
        public bool? UStatus { get; set; }
        public bool? UGender { get; set; }
        public DateTime? UDob { get; set; }

        public User(int uId, string uEmail, string uPassword, int? rId, string uWallet, string uUsername, bool? uStatus, bool? uGender, DateTime? uDob)
        {
            UId = uId;
            UEmail = uEmail;
            UPassword = uPassword;
            RId = rId;
            UWallet = uWallet;
            UUsername = uUsername;
            UStatus = uStatus;
            UGender = uGender;
            UDob = uDob;
        }

        public virtual Role RIdNavigation { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
