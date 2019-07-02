using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSystem.Models
{
    public class CodeService
    { 
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
         private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
         }

        /// <summary>
        /// 取得書本資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookClass(string classId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT BOOK_CLASS_ID AS ClssId,bc.BOOK_CLASS_NAME AS ClassName
                            FROM BOOK_CLASS bc WHERE bc.BOOK_CLASS_ID != @CLassId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CLassId", classId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        /// <summary>
        /// 取得codeTable的部分資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCodeTable(string classId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT DISTINCT BOOK_CLASS_ID AS CodeId, BOOK_CLASS_NAME AS CodeName 
                           FROM BOOK_CLASS WHERE bc.BOOK_CLASS_ID != @CLassId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CLassId", classId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }


        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeId"].ToString() ,
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
   
}