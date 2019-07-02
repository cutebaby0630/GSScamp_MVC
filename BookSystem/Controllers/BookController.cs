using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSystem.Controllers
{
    public class BookController : Controller
    {
        /// <summary>
        /// 員工資料查詢
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 員工資料查詢(查詢)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookServiceArg arg)
        {
            Models.BookService bookService = new Models.BookService();
            ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);
            return View("Index");
        }
    }
}