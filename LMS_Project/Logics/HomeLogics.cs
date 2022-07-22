using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Logics
{
    public class HomeLogics
    {
        LMSContext db;

        public HomeLogics()
        {
            db = new LMSContext();
        }
        public List<BookCategory> GetAllBCate()
        {
            return db.BookCategories.ToList();
        }
        public List<Book> GetAllB()
        {
            return db.Books.ToList();
        }
        public List<Book> GetAllBByBCateId(string bcid)
        {
            return db.Books.Where(b => b.BCateId == bcid).ToList();
        }
        public List<Book> GetAllBByBCateAut(string bcid, int autid)
        {
            return db.Books.Where(b => b.BCateId == bcid && b.AutId == autid).ToList();
        }
        public IEnumerable<Book> GetTop4BLast()
        {
            return db.Books.OrderByDescending(b => b.BLastupdated).Take(4).ToList();
        }
        public IEnumerable<Book> GetTop4BBestBor()
        {
            return db.Books.OrderByDescending(b => b.BNumBorrow).Take(4).ToList();
        }
        public Book GetBookById(int bid)
        {
            return db.Books.FirstOrDefault(b => b.BId == bid);
        }
        public BookCategory GetBookCateById(string bcid)
        {
            return db.BookCategories.FirstOrDefault(bc => bc.BCateId == bcid);
        }
    }
}
