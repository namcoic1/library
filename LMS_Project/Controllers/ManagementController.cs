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
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Book(string bcid = "0", int autid = -1, int page = 1, int bid = 0)
        {
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (u == null) return Redirect("/user/account/log");
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            IEnumerable<Book> books = null;
            int numPerPage = 4, numPage = 0, size = 0, minisize = 0;
            if (!bcid.Equals("0") && autid > -1)
            {
                size = hl.GetAllBStaByBCate(bcid, u.UId, autid).Count;
                numPage = size / numPerPage;
                if (size > 4 && numPage % 4 != 0 && size % 4 != 0) numPage += 1;
                else if (size > 0 && size <= 4) numPage = 1;
                books = hl.GetAllBStaByBCate(bcid, u.UId, autid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            else if (!bcid.Equals("0"))
            {
                size = hl.GetAllBByBCateIdAct(bcid, u.UId).Count;
                numPage = size / numPerPage;
                if (size > 4 && numPage % 4 != 0 && size % 4 != 0) numPage += 1;
                else if (size > 0 && size <= 4) numPage = 1;
                books = hl.GetAllBByBCateIdAct(bcid, u.UId).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            else if (autid > -1)
            {
                size = autid == 0 ? hl.GetAllBDis(u.UId).Count : hl.GetAllBAct(u.UId).Count;
                numPage = size / numPerPage;
                if (size > 4 && numPage % 4 != 0 && size % 4 != 0) numPage += 1;
                else if (size > 0 && size <= 4) numPage = 1;
                books = autid == 0 ? hl.GetAllBDis(u.UId).Skip((int)(numPerPage * (page - 1))).Take(numPerPage) : hl.GetAllBAct(u.UId).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            else
            {
                size = hl.GetAllBActByAut(u.UId).Count;
                numPage = size / numPerPage;
                if (size > 4 && numPage % 4 != 0 && size % 4 != 0) numPage += 1;
                else if (size > 0 && size <= 4) numPage = 1;
                books = hl.GetAllBActByAut(u.UId).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            foreach (Book b in books)
            {
                b.BPrice = (decimal?)((double)b.BPrice + (double)b.BPrice * 0.2);
            }
            if (bid == 1) ViewBag.Suc = "You have already modified status of book successfully!";
            ViewBag.B = books;
            ViewBag.BCateCur = bcid;
            ViewBag.BStatus = autid;
            ViewBag.TotalSize = size;
            ViewBag.MiniSize = minisize;
            ViewBag.PageCur = page;
            ViewBag.NumPage = numPage;
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return View("/Views/Index/BookManage.cshtml");
        }
        public IActionResult User(int bcid = 0, int autid = -1, int page = 1, int bid = 0)
        {
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (u == null) return Redirect("/user/account/log");
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            List<Role> rols = ul.GetAllRol();
            IEnumerable<User> users = null;
            int numPerPage = 3, numPage = 0, size = 0, minisize = 0;
            if (bcid > 0 && autid > -1)
            {
                size = ul.GetAllRSta(bcid, autid).Count;
                numPage = size / numPerPage;
                if (size > 3 && numPage % 3 != 0 && size % 3 != 0) numPage += 1;
                else if (size > 0 && size <= 3) numPage = 1;
                users = ul.GetAllRSta(bcid, autid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = users.Count();
            }
            else if (bcid > 0)
            {
                size = ul.GetAllUserByRid(bcid).Count;
                numPage = size / numPerPage;
                if (size > 3 && numPage % 3 != 0 && size % 3 != 0) numPage += 1;
                else if (size > 0 && size <= 3) numPage = 1;
                users = ul.GetAllUserByRid(bcid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = users.Count();
            }
            else if (autid > -1)
            {
                size = ul.GetAllUser(autid).Count;
                numPage = size / numPerPage;
                if (size > 3 && numPage % 3 != 0 && size % 3 != 0) numPage += 1;
                else if (size > 0 && size <= 3) numPage = 1;
                users = ul.GetAllUser(autid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = users.Count();
            }
            else
            {
                size = ul.GetAllUser().Count;
                numPage = size / numPerPage;
                if (size > 3 && numPage % 3 != 0 && size % 3 != 0) numPage += 1;
                else if (size > 0 && size <= 3) numPage = 1;
                users = ul.GetAllUser().Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = users.Count();
            }
            if (bid == 1) ViewBag.Suc = "You have already modified status of user successfully!";
            ViewBag.User = users;
            ViewBag.Role = rols;
            ViewBag.UStatus = autid;
            ViewBag.TotalSize = size;
            ViewBag.MiniSize = minisize;
            ViewBag.RCur = bcid;
            ViewBag.PageCur = page;
            ViewBag.NumPage = numPage;
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return View("/Views/Index/UserManage.cshtml");
        }
        public IActionResult BookDis(string bcid = "0", int autid = -1, int page = 1, int bid = 0)
        {
            HomeLogics hl = new HomeLogics();
            BookLogics bl = new BookLogics();
            Book b = hl.GetBookById(bid);
            if (b == null) ;
            else if (b.BStatus == true) bl.DisBook(b);
            else bl.ActBook(b);
            return RedirectToAction("book", new { bcid = bcid, autid = autid, page = page, bid = 1 });
        }
        public IActionResult UserDis(string bcid = "0", int autid = -1, int page = 1, int bid = 0)
        {
            UserLogics ul = new UserLogics();
            User b = ul.GetUserById(bid);
            if (b == null);
            else if (b.UStatus == true) ul.DisUser(b);
            else ul.ActUser(b);
            return RedirectToAction("user", new { bcid = bcid, autid = autid, page = page, bid = 1 });
        }
    }
}
