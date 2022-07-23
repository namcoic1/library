using System;
using System.Collections.Generic;

#nullable disable

namespace LMS_PRN_Project.Models
{
    public partial class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public string BCateId { get; set; }
        public string BCateName { get; set; }
        public string BCateImg { get; set; }
        public string BCateDes { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
