using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using BookStore.Util;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        // создаем контекст данных
        BookContext db = new BookContext();
        SquareContext sqdb = new SquareContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

        public string IndexContext()
        {
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.Write("<h1>Hello World</h1>");

            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>" +
                "<br><td><a href = \"/Home/Index\"> Главная</a></td></br>";
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = getToday();
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!" +
                "<br><td><a href = \"/Home/Index\"> Главная</a></td></br>";
        }
        private DateTime getToday()
        {
            return DateTime.Now;
        }

        /// ///////////////////////////////////////////////////////
        ///////////////////////////////////формула
        [HttpGet]
        public ActionResult SquareOf()
        {
            IEnumerable<Square> squares = sqdb.Squares;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Squares = squares;
            return View();
        }

        [HttpPost]
        public string SquareOf(Square square)
        {
            
            int a = square.a;
            int h = square.h;
            square.count(a, h);
            double s1 = square.s1;

            square.Date = getToday();
            sqdb.Squares.Add(square);
            sqdb.SaveChanges();

            return "<h2>Площадь треугольника с основанием " + a +
                    " и высотой " + h + " равна " + s1 + "</h2>" + 
                    "<td><a href = \"/Home/SquareOf\"> Еще раз</a></td>" +
                    "<br><td><a href = \"/Home/Index\"> Главная</a></td></br>";
        }

        public ActionResult IndexSquares()
        {
            // получаем из бд все объекты Square
            IEnumerable<Square> squares = sqdb.Squares;
            // передаем все объекты в динамическое свойство Squares в ViewBag
            ViewBag.Squares = squares;
            // возвращаем представление
            return View();
        }
        ///////////////////////////////////формула
        /// ///////////////////////////////////////////////////////

        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2.0;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }

        public ActionResult IndexPurchased()
        {
            // получаем из бд все объекты Purchase
            IEnumerable<Purchase> purchases = db.Purchases;
            // передаем все объекты в динамическое свойство Purchase в ViewBag
            ViewBag.Purchases = purchases;
            // возвращаем представление
            return View();
        }


        // Отправка потока
        public FileResult GetStream()
        {
            string path = Server.MapPath("~/Files/videoplayback.mp4");
            // Объект Stream
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "videoplayback/mp4";
            string file_name = "videoplayback.mp4";
            return File(fs, file_type, file_name);
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!</h2>");
        }

        public ActionResult GetImage()
        {
            string path = "../Images/giphy.gif";
            return new ImageResult(path);
        }

        public ViewResult SomeMethodData()
        {
            ViewData["Head"] = "Привет мир!";
            return View("SomeView");
        }

        public ViewResult SomeMethodBag()
        {
            ViewBag.Head = "Привет мир!";
            return View("SomeView");
        }
    }
}

