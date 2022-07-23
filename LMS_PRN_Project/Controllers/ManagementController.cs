using LMS_PRN_Project.Logics;
using LMS_PRN_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_PRN_Project.Controllers
{
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Book(string bcid = "0", int autid = -1, int page = 1)
        {
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (u == null) return Redirect("/user/account/log");
            HomeLogics hl = new HomeLogics();
            AuthorLogics al = new AuthorLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<Author> auts = al.GetAllAutAd();
            IEnumerable<Book> books = null;
            int numPerPage = 5, numPage = 0, size = 0, minisize = 0;
            if (!bcid.Equals("0") && autid > -1)
            {
                size = hl.GetAllBByBCateBSta(bcid, autid).Count;
                numPage = size / numPerPage;
                if (size > 5 && numPage % 5 != 0 && size % 5 != 0) numPage += 1;
                else if (size > 0 && size <= 5) numPage = 1;
                books = hl.GetAllBByBCateBSta(bcid, autid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
            }
            else if (!bcid.Equals("0"))
            {
                size = hl.GetAllBByBCateIdAd(bcid).Count;
                numPage = size / numPerPage;
                if (size > 5 && numPage % 5 != 0 && size % 5 != 0) numPage += 1;
                else if (size > 0 && size <= 5) numPage = 1;
                books = hl.GetAllBByBCateIdAd(bcid).Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            else if (autid > -1)
            {
                size = autid == 0? hl.GetAllBDis().Count:hl.GetAllB().Count;
                numPage = size / numPerPage;
                if (size > 5 && numPage % 5 != 0 && size % 5 != 0) numPage += 1;
                else if (size > 0 && size <= 5) numPage = 1;
                books = autid == 0 ? hl.GetAllBDis().Skip((int)(numPerPage * (page - 1))).Take(numPerPage): hl.GetAllB().Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            else
            {
                size = hl.GetAllBAd().Count;
                numPage = size / numPerPage;
                if (size > 5 && numPage % 5 != 0 && size % 5 != 0) numPage += 1;
                else if (size > 0 && size <= 5) numPage = 1;
                books = hl.GetAllBAd().Skip((int)(numPerPage * (page - 1))).Take(numPerPage);
                minisize = books.Count();
            }
            foreach (Book b in books)
            {
                b.BPrice = (decimal?)((double)b.BPrice + (double)b.BPrice * 0.2);
            }
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
    }
}
