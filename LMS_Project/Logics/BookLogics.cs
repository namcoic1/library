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
        public void EditBook(Book b)
        {
            Book old = db.Books.FirstOrDefault(bb => bb.BId == b.BId);
            old.BName = b.BName;
            old.BStock = b.BStock;
            old.BPrice = b.BPrice;
            old.BDesc = b.BDesc;
            old.BCateId = b.BCateId;
            old.BLastupdated = DateTime.Now;
            db.SaveChanges();
        }
    }
}
