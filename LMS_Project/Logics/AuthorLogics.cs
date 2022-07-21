using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Logics
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
            return db.Authors.ToList();
        }
        public List<Book> GetAllBByAutId(int autid)
        {
            return db.Books.Where(b => b.AutId == autid).ToList();
        }
    }
}
