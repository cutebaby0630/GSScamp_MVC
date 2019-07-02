using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    public class Book_Data
    {
      
        public int BookId { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        public string ClassName { get; set; }

         /// <summary>
        /// 書本名稱 
        /// </summary>
        public string BookName { get; set; }
      
        /// <summary>
        /// 書本作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 購買日期
        /// </summary>
        public string BoughtDate { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 書本附加物品
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 書本狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 借閱人ID
        /// </summary>
        public string Keeper { get; set; }

        /// <summary>
        /// 借閱人英文名
        /// </summary>
        public string KeeperName { get; set; }

        /// <summary>
        /// 狀態可否借出
        /// </summary>
        public string CodeName { get; set; }

    }
}