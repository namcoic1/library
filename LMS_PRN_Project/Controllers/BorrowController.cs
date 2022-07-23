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
    public class BorrowController : Controller
    {
        public IActionResult Index(int bcid = 1)
        {
            HomeLogics hl = new HomeLogics();
            AuthorLogics al = new AuthorLogics();
            BorrowLogics bl = new BorrowLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<Author> auts = al.GetAllAut();
            int numPerPage = 4, size = 0, numPage = 0;
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (u==null) return Redirect("/user/account/log");
            List<MyBorrowed> myborrow = new List<MyBorrowed>();
            foreach(Borrow bo in bl.GetAllBorByUid(u.UId))
            {
                foreach(BorrowDetail bor in bl.GetAllBorByBorid(bo.BrId))
                {
                    Book book = hl.GetBookById(bor.BId);
                    MyBorrowed mb = new MyBorrowed(book, bo, bor);
                    myborrow.Add(mb);
                }
            }
            size = myborrow.Count;
            numPage = size / numPerPage;
            if (size > 4 && numPage % 4 != 0 && size % 4 != 0) numPage += 1;
            else if (size > 0 && size <= 4) numPage = 1;
            IEnumerable<MyBorrowed> listbr = myborrow.Skip((int)(numPerPage * (bcid - 1))).Take(numPerPage);
            ViewBag.MyBorrow = listbr;
            ViewBag.TotalSize = myborrow.Count;
            ViewBag.MiniSize = listbr.Count();
            ViewBag.PageCur = bcid;
            ViewBag.NumPage = numPage;
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return View("/Views/Index/MyBorrow.cshtml");
        }
    }
}
