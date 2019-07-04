using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookSystem.Models
{
    public class LendData
    {
        [DisplayName("借閱日期")]
        public string LendDate { get; set; }
        [DisplayName("借閱人編號")]
        public string KeeperId { get; set; }
        [DisplayName("英文姓名")]
        public string KeeperName { get; set; }
        [DisplayName("中文姓名")]
        public string KeeperCName { get; set; }
    }
}