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
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCart(string bcid, int autid)
        {
            Dictionary<int, int> cart;
            int quantity = 1, sizeCart = 0;
            HomeLogics hl = new HomeLogics();
            Book book = hl.GetBookById(autid);
            if (Request.Cookies["cart"] != null)
            {
                int check = 0;
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                foreach (int key in cart.Keys)
                {
                    if (key == book.BId)
                    {
                        check = 1;
                        if (cart[key] < book.BStock)
                        {
                            cart[key] += 1;
                        }
                        break;
                    }
                }
                if (check == 0 && book.BStock > 0)
                {
                    cart.Add(book.BId, quantity);
                }
                sizeCart = cart.Count;
            }
            else
            {
                cart = new Dictionary<int, int>();
                if (book.BStock > 0)
                {
                    cart.Add(book.BId, quantity);
                }
                sizeCart = cart.Count;
            }
            var cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            if (bcid.Equals("homie")) return Redirect("/home/index");
            else if (bcid.Equals("list")) return Redirect("/home/booklist");
            else return Redirect("/home/bookdetail/" + autid);
        }
        public IActionResult ViewCart(int bcid = 1, int autid = -1)
        {
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            int numPerPage = 2, size = 0, numPage = 0;
            double total = 0;
            List<Cart> carts = new List<Cart>();
            if (Request.Cookies["cart"] != null)
            {
                Dictionary<int, int> cok = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                foreach (int key in cok.Keys)
                {
                    Book book = hl.GetBookById(key);
                    Cart cart = new Cart(book, cok[key]);
                    cart.Book.BPrice += (decimal?)((double)cart.Book.BPrice * 0.2);
                    cart.TotalAt = String.Format("{0:0.00}", (double)(book.BPrice * cok[key]));
                    carts.Add(cart);
                    total += (double)(book.BPrice * cok[key]);
                }
                size = carts.Count;
                numPage = size / numPerPage;
                if (size > 2 && numPage % 2 != 0 && size % 2 != 0) numPage += 1;
                else if (size > 0 && size <= 2) numPage = 1;
                IEnumerable<Cart> listCart = carts.Skip(numPerPage * (bcid - 1)).Take(numPerPage);
                ViewBag.Cart = listCart;
                ViewBag.PageCur = bcid;
                ViewBag.NumPage = numPage;
                ViewBag.TotalSize = size;
                ViewBag.MiniSize = listCart.Count();
                ViewBag.Total = String.Format("{0:0.00}", total);
            }
            if (autid == 1)
            {
                string suc = "Congratulations, you have already check out books in cart successfully. Thank you very much and have a nice day!";
                ViewBag.CheckSuc = suc;
            }
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return View("/Views/Index/BookCart.cshtml");
        }
        public IActionResult Process(string bcid, int autid)
        {
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            Book book = hl.GetBookById(autid);
            if (Request.Cookies["cart"] != null)
            {
                Dictionary<int, int> cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                if (cart.ContainsKey(autid))
                {
                    if (bcid.Equals("desc"))
                    {
                        if (cart[autid] == 1) cart.Remove(autid);
                        else cart[autid] -= 1;
                    }
                    else
                    {
                        if (cart[autid] < book.BStock)
                        {
                            cart[autid] += 1;
                        }
                    }
                }
                var cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
                if (cart.Count == 0) cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(0) };
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            }
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return RedirectToAction("viewcart");
        }
        public IActionResult DelCart(int bcid)
        {
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            if (Request.Cookies["cart"] != null)
            {
                Dictionary<int, int> cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                cart.Remove(bcid);
                var cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
                if (cart.Count == 0) cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(0) };
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            }
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return RedirectToAction("viewcart");
        }
        public IActionResult Checkout()
        {
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            List<BookCategory> bcates = hl.GetAllBCate();
            List<User> auts = ul.GetAllAut();
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (u == null) return Redirect("/user/account/log");
            List<Cart> carts = new List<Cart>();
            double total = 0;
            if (Request.Cookies["cart"] != null)
            {
                Dictionary<int, int> cok = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                foreach (int key in cok.Keys)
                {
                    Book book = hl.GetBookById(key);
                    Cart cart = new Cart(book, cok[key]);
                    cart.Book.BPrice += (decimal?)((double)cart.Book.BPrice * 0.2);
                    cart.TotalAt = String.Format("{0:0.00}", (double)(book.BPrice * cok[key]));
                    carts.Add(cart);
                    total += (double)(book.BPrice * cok[key]);
                }
                ViewBag.Cart = carts;
                ViewBag.Total = String.Format("{0:0.00}", total);
            }
            ViewBag.BCate = bcates;
            ViewBag.Aut = auts;
            return View("/Views/Index/Checkout.cshtml");
        }
        [HttpPost]
        public IActionResult AccessCheck(string totalmon)
        {
            LMSContext db = new LMSContext();
            HomeLogics hl = new HomeLogics();
            UserLogics ul = new UserLogics();
            string json = HttpContext.Session.GetString("user");
            User u = null;
            if (json != null) u = JsonConvert.DeserializeObject<User>(json);
            if (Request.Cookies["cart"] != null)
            {
                Dictionary<int, int> cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["cart"]);
                Borrow o = new Borrow(u.UId, DateTime.Now, DateTime.Now.AddDays(3), 1);
                db.Borrows.Add(o);
                db.SaveChanges();
                int lastOrId = db.Borrows.OrderBy(x => x.BrId).LastOrDefault().BrId;
                foreach (int key in cart.Keys)
                {
                    Book p = hl.GetBookById(key);
                    double price = (double)p.BPrice + (double)p.BPrice * 0.2;
                    double total = (double)(price * cart[key]);
                    BorrowDetail od = new BorrowDetail(p.BId, lastOrId, cart[key], (decimal?)price, (decimal?)total, true);
                    db.BorrowDetails.Add(od);
                    double pay = Double.Parse(u.UWallet) - Double.Parse(totalmon);
                    u.UWallet = pay.ToString();
                    p.BStock -= cart[key];
                    p.BNumBorrow += cart[key];
                    db.Books.Update(p);
                    db.Users.Update(u);
                    db.SaveChanges();
                }
                cart.Clear();
                var cookieOptions = new CookieOptions { Expires = DateTime.Now.AddDays(0) };
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), cookieOptions);
            }
            return RedirectToAction("viewcart", new { bcid = 1, autid = 1 });
        }
    }
}
