using LMS_PRN_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_PRN_Project.Logics
{
    public class AuthorLogics
    {
        LMSContext db;

        public AuthorLogics()
        {
            db = new LMSContext();
        }
        public List<Author> GetAllAut()
        {
            return db.Authors.Where(a => a.AutStatus == true).ToList();
        }
        public List<Author> GetAllAutAd()
        {
            return db.Authors.ToList();
        }
        public Author GetAutById(int autid)
        {
            return db.Authors.FirstOrDefault(a => a.AutId == autid);
        }
        public List<Book> GetAllBByAutId(int autid)
        {
            return db.Books.Where(b => b.AutId == autid && b.BStatus == true).ToList();
        }
    }
}
