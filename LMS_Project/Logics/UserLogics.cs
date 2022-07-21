using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Logics
{
    public class UserLogics
    {
        LMSContext db;

        public UserLogics()
        {
            db = new LMSContext();
        }
        public User GetUserLog(string email, string pass)
        {
            return db.Users.FirstOrDefault(u => u.UEmail.Equals(email) && u.UPassword.Equals(pass));
        }
        public User GetUserReg(string email)
        {
            return db.Users.FirstOrDefault(u => u.UEmail.Equals(email));
        }
        public void AddUser(User u)
        {
            u.UId = 0;
            db.Users.Add(u);
            db.SaveChanges();
        }

    }
}
