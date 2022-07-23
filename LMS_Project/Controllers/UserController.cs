using LMS_Project.Logics;
using LMS_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Account(string bcid)
        {
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (bcid.Equals("log")) ViewBag.Log = "Login";
            else if (bcid.Equals("reg"))
            {
                if (u != null) return Redirect("/home/index");
                else ViewBag.Reg = "Register";
            }
            else if (bcid.Equals("pro"))
            {
                if (u == null) return RedirectToAction("account", new { bcid = "log" });
                else return View("/Views/Index/Profile.cshtml");
            }
            else if (bcid.Equals("change"))
            {
                if (u == null) return RedirectToAction("account", new { bcid = "log" });
                else return View("/Views/Index/ChangePass.cshtml");
            }
            else
            {
                HttpContext.Session.Remove("user");
                return Redirect("/home/index");
            }
            return View("/Views/Index/Reg_Log.cshtml");
        }
        [HttpPost]
        public IActionResult AccessLogin(string uemail, string upass)
        {
            UserLogics ul = new UserLogics();
            User u = ul.GetUserLog(uemail, upass);
            if (u == null)
            {
                ViewBag.Log = "Login";
                ViewBag.Err = "Email or Password is invalid!";
                return View("/Views/Index/Reg_Log.cshtml");
            }
            else if (u != null && u.UStatus == false)
            {
                ViewBag.Log = "Login";
                ViewBag.Err = "Your account is banned. Please contact for us to know more!";
                return View("/Views/Index/Reg_Log.cshtml");
            }
            else
            {
                string user = JsonConvert.SerializeObject(u);
                HttpContext.Session.SetString("user", user);
                return Redirect("/home/index");
            }
        }
        [HttpPost]
        public IActionResult AccessRegister(string uname, string uemail, string upass, string ugender)
        {
            UserLogics ul = new UserLogics();
            User u = ul.GetUserReg(uemail);
            if (u != null)
            {
                ViewBag.Reg = "Register";
                ViewBag.Err = "Email is existing!";
                return View("/Views/Index/Reg_Log.cshtml");
            }
            else
            {
                User unew = new User(9999, uemail, upass, 2, "9999", uname, true, ugender.Equals("1") ? true : false, DateTime.Now.Date, 1);
                ul.AddUser(unew);
                return RedirectToAction("account", new { bcid = "log" });
            }
        }
        [HttpPost]
        public IActionResult AccessProfile(string uname, string ugender, string udob, string uphone, string uaddress)
        {
            UserLogics ul = new UserLogics();
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            User unew = new User(u.UId, u.UEmail, u.UPassword, u.RId, u.UWallet, uname, u.UStatus, ugender.Equals("1") ? true : false, DateTime.Parse(udob), 1);
            unew.UPhone = uphone; unew.UAddress = uaddress;
            ul.UpdateUserPro(unew);
            string user = JsonConvert.SerializeObject(unew);
            HttpContext.Session.SetString("user", user);
            ViewBag.Suc = "You have just updated profile successfully!";
            return View("/Views/Index/Profile.cshtml");
        }
        [HttpPost]
        public IActionResult AccessChange(string uoldpass, string unewpass, string ucfpass)
        {
            UserLogics ul = new UserLogics();
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (!uoldpass.Equals(u.UPassword))
            {
                ViewBag.Err = "Old password is wrong!";
            }
            else if (uoldpass.Equals(unewpass))
            {
                ViewBag.Err = "New password has existed before!";
            }
            else if (!unewpass.Equals(ucfpass))
            {
                ViewBag.Err = "Confirm password is wrong!";
            }
            else
            {
                User unew = new User(u.UId, u.UEmail, unewpass, u.RId, u.UWallet, u.UUsername, u.UStatus, u.UGender, u.UDob, 1);
                unew.UPhone = unew.UPhone; unew.UAddress = u.UAddress;
                ul.UpdateUserPass(unew);
                string user = JsonConvert.SerializeObject(unew);
                HttpContext.Session.SetString("user", user);
                ViewBag.Suc = "You have just changed password successfully!";
            }
            return View("/Views/Index/ChangePass.cshtml");
        }
    }
}
