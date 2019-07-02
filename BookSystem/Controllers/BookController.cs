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
            ViewBag.JobTitleCodeData = this.codeService.GetCodeTable("TITLE");
            ViewBag.CountryCodeData = this.codeService.GetCodeTable("COUNTRY");
            ViewBag.CityCodeData = this.codeService.GetCodeTable("CsITY");

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
                Models.BookService EmployeeService = new Models.BookService();
                EmployeeService.DeleteEmployeeById(bookId);
                return this.Json(true);
            }

            catch (Exception ex)
            {
                return this.Json(false);
            }
        }
    }
    }
