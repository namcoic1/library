using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Models
{
    public class MyBorrowed
    {
        Book book;
        Borrow bor;
        BorrowDetail br;

        public MyBorrowed()
        {
        }

        public MyBorrowed(Book book, Borrow bor, BorrowDetail br)
        {
            this.book = book;
            this.bor = bor;
            this.br = br;
        }

        public Book Book
        {
            get { return book; }
            set { book = value; }
        }
        public Borrow Borrow
        {
            get { return bor; }
            set { bor = value; }
        }
        public BorrowDetail BorrowDetail
        {
            get { return br; }
            set { br = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
