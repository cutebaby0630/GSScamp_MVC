using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    public class BookData
    {
      
        public int BookId { get; set; }

        /// <summary>
        /// 類別名稱ID
        /// </summary>
        [DisplayName("圖書類別")]
        public string ClassId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]

        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("圖書類別")]
        public string ClassName { get; set; }

        /// <summary>
        /// 書本名稱 
        /// </summary>
        [DisplayName("書名")]
       [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        /// <summary>
        /// 書本作者
        /// </summary>
        [DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookAuthor { get; set; }

        /// <summary>
        /// 書本內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        public string BookNote { get; set; }

        /// <summary>
        /// 購買日期
        /// </summary>
        [DisplayName("購書日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BoughtDate { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        [DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string Publisher { get; set; }

        /// <summary>
        /// 書本附加物品
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 書本狀態Id
        /// </summary>
        public string CodeId { get; set; }

        /// <summary>
        /// 書本狀態
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// 借閱人ID
        /// </summary>
        public string KeeperId { get; set; }

        /// <summary>
        /// 借閱人英文名
        /// </summary>
        public string KeeperName { get; set; }

   

    }
}