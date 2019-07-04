using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookSystem.Controllers
{
    public class BookController : Controller
    {
        Models.CodeService codeService = new Models.CodeService();

        /// <summary>
        /// 員工資料查詢
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.ClassName = this.codeService.GetBookClassId("ClassName");
            ViewBag.KeeperName = this.codeService.GetMember("KeeperName");
            ViewBag.CodeName = this.codeService.GetCodeName("CodeName");
            return View();
        }

        /// <summary>
        /// 書本資料查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.BookServiceArg arg)
        {
            Models.BookService bookService = new Models.BookService();
            ViewBag.SearchResult = bookService.GetBookByCondtioin(arg);
            ViewBag.ClassName = this.codeService.GetBookClassId("ClassName");
            ViewBag.KeeperName = this.codeService.GetMember("KeeperName");
            ViewBag.CodeName = this.codeService.GetCodeName("CodeName");
            return View("Index");
        }

        /// <summary>
        /// 刪除員工
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteBook(string bookId)
        {
            try
            {
                Models.BookService BookService = new Models.BookService();
                BookService.DeleteEmployeeById(bookId);
                return this.Json(true);
            }

            catch (Exception )
            {
                return this.Json(false);
            }
        }
        /// <summary>
        /// 新增畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertBook()
        {
            ViewBag.ClassName = this.codeService.GetBookClassId("ClassName");
            return View(new Models.BookData());
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertBook(Models.BookData bookdata)
        {
            ViewBag.ClassName = this.codeService.GetBookClassId("ClassName");
            if (ModelState.IsValid)
            {
                Models.BookService bookService = new Models.BookService();
                bookService.InsertBook(bookdata);
                TempData["message"] = "存檔成功";
            }
            return View(bookdata);
        }
    }
 }
