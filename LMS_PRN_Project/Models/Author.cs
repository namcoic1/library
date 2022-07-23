using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_PRN_Project.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AutId { get; set; }
        public string AutName { get; set; }
        public bool? AutGender { get; set; }
        public DateTime? AutDob { get; set; }
        public string AutPhone { get; set; }
        public string AutAddress { get; set; }
        public int? UId { get; set; }
        public bool? AutStatus { get; set; }

        public virtual User UIdNavigation { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
