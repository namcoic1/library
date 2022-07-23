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
        public List<Role> GetAllRol()
        {
            return db.Roles.ToList();
        }
        public Role GetRolById(int rid)
        {
            return db.Roles.FirstOrDefault(r => r.RId == rid);
        }
        public List<User> GetAllUserByRid(int rid)
        {
            return db.Users.Where(u => u.RId == rid).OrderBy(x => x.RId).ToList();
        }
        public List<User> GetAllRSta(int rid, int status)
        {
            if (status == 0) return db.Users.Where(u => u.UStatus == false && u.RId == rid).OrderBy(x => x.RId).ToList();
            else if (status == 1) return db.Users.Where(u => u.UStatus == true && u.RId == rid).OrderBy(x => x.RId).ToList();
            else return db.Users.Where(u => u.RId == rid).OrderBy(x => x.RId).ToList();
        }
        public List<User> GetAllUser(int status)
        {
            if (status == 0) return db.Users.Where(u => u.UStatus == false && u.RId != 1).OrderBy(x => x.RId).ToList();
            else if (status == 1) return db.Users.Where(u => u.UStatus == true && u.RId != 1).OrderBy(x => x.RId).ToList();
            else return db.Users.Where(u => u.RId != 1).OrderBy(x => x.RId).ToList();
        }
        public List<User> GetAllUser()
        {
            return db.Users.Where(u => u.RId != 1).ToList();
        }
        public List<User> GetAllAut()
        {
            return db.Users.Where(u => u.RId == 4 && u.UStatus == true).ToList();
        }
        public User GetUserLog(string email, string pass)
        {
            return db.Users.FirstOrDefault(u => u.UEmail.Equals(email) && u.UPassword.Equals(pass));
        }
        public User GetUserReg(string email)
        {
            return db.Users.FirstOrDefault(u => u.UEmail.Equals(email));
        }
        public User GetUserById(int uid)
        {
            return db.Users.FirstOrDefault(u => u.UId == uid);
        }
        public void AddUser(User u)
        {
            u.UId = 0;
            db.Users.Add(u);
            db.SaveChanges();
        }
        public void UpdateUserPro(User unew)
        {
            User oldU = db.Users.FirstOrDefault(oldu => oldu.UId == unew.UId);
            oldU.UPhone = unew.UPhone;
            oldU.UAddress = unew.UAddress;
            oldU.UUsername = unew.UUsername;
            oldU.UGender = unew.UGender;
            oldU.UDob = unew.UDob;
            db.SaveChanges();
        }
        public void UpdateUserPass(User unew)
        {
            User oldU = db.Users.FirstOrDefault(oldu => oldu.UId == unew.UId);
            oldU.UPassword = unew.UPassword;
            db.SaveChanges();
        }
        public void DisUser(User b)
        {
            User old = db.Users.FirstOrDefault(bb => bb.UId == b.UId);
            old.UStatus = false;
            db.SaveChanges();
        }
        public void ActUser(User b)
        {
            User old = db.Users.FirstOrDefault(bb => bb.UId == b.UId);
            old.UStatus = true;
            db.SaveChanges();
        }
    }
}
