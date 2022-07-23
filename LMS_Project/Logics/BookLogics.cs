using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Logics
{
    public class BookLogics
    {
        LMSContext db;

        public BookLogics()
        {
            db = new LMSContext();
        }
        public void DisBook(Book b)
        {
            Book old = db.Books.FirstOrDefault(bb => bb.BId == b.BId);
            old.BStatus = false;
            old.BLastupdated = DateTime.Now;
            db.SaveChanges();
        }
        public void ActBook(Book b)
        {
            Book old = db.Books.FirstOrDefault(bb => bb.BId == b.BId);
            old.BStatus = true;
            old.BLastupdated = DateTime.Now;
            db.SaveChanges();
        }
    }
}
