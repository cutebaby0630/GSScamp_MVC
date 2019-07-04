using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSystem.Models
{
    public class BookServiceArg
    {
        [DisplayName("書名")]
        public string BookName { get; set; }
        [DisplayName("圖書類別")]
        public string ClassName { get; set; }
        [DisplayName("購書日期")]
        public string BoughtDate { get; set; }
        [DisplayName("借閱狀態")]
        public string CodeName { get; set; }
        [DisplayName("借閱人")]
        public string KeeperName { get; set; }
        [DisplayName("作者")]
        public string BookAuthor { get; set; }
        [DisplayName("出版商")]
        public string Publisher { get; set; }
         [DisplayName("內容簡介")]
        public string BookNote { get; set; }
        public string CodeId { get; set; }
        public string ClassId { get; set; }
        public string BookId { get; set; }
    }
}