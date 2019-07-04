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
        /// 取得Book_Class_Name的部分資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookClassId(string ClassName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT DISTINCT BOOK_CLASS_ID AS ClassId, BOOK_CLASS_NAME AS ClassName 
                           FROM BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@ClassName", ClassName));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapClassName(dt);
        }

        /// <summary>
        /// Maping書本分類代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapClassName(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["ClassName"].ToString() ,
                    Value = row["ClassId"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// 取得借閱人資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetMember(string KeeperName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT USER_ID AS KeeperId,USER_ENAME AS KeeperName FROM MEMBER_M";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@KeeperName", KeeperName));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapKeeperName(dt);
        }

        /// <summary>
        /// Maping 借閱人代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapKeeperName(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["KeeperName"].ToString(),
                    Value = row["KeeperId"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// 取得借閱狀態資料
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCodeName(string CodeName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT bc.CODE_ID AS CodeId, bc.CODE_NAME AS CodeName 
                           FROM BOOK_CODE bc 
                           WHERE bc.CODE_TYPE = 'BOOK_STATUS'";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CodeName", CodeName));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeName(dt);
        }

        /// <summary>
        /// Maping 借閱狀態代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<SelectListItem> MapCodeName(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
   
}