using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookSystem.Models
{
    public class BookService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        /// <summary>
        /// 依照條件取得資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Book_Data> GetBookByCondtioin(Models.BookServiceArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT 
	                        bc.BOOK_CLASS_NAME AS BookClassName,
                            bd.BOOK_NAME AS BookName,
                            CONVERT(VARCHAR(12),bd.BOOK_BOUGHT_DATE,23) AS BoughtDate,
                            bc1.CODE_NAME AS CodeName,
                            ISNULL(mm.USER_ENAME,'') AS KeeperName 
                           FROM BOOK_DATA bd 
	                       JOIN BOOK_CLASS bc
                                ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
	                       JOIN BOOK_CODE bc1 
                                ON bc1.CODE_ID = bd.BOOK_STATUS AND bc1.code_type = 'BOOK_STATUS'
	                       LEFT JOIN MEMBER_M mm 
                                ON bd.BOOK_KEEPER = mm.USER_ID
                           Where (bd.BOOK_NAME LIKE '%'+ @BookName +'%') OR
                                 (bc.BOOK_CLASS_NAME = @BookClassName) OR
                                 (mm.USER_ENAME = @KeeperName) OR
                                 (bc1.CODE_NAME = @CodeName)
                           ORDER BY bc.BOOK_CLASS_NAME;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookClassName", arg.ClassName == null ? string.Empty : arg.ClassName));
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BoughtDate", arg.BoughtDate == null ? "1900/01/01" : arg.BoughtDate));
                cmd.Parameters.Add(new SqlParameter("@CodeName", arg.CodeName == null ? string.Empty : arg.CodeName));
                cmd.Parameters.Add(new SqlParameter("@KeeperName", arg.KeeperName == null ? string.Empty : arg.KeeperName));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        public void DeleteEmployeeById(string BookId)
        {
            try
            {
                string sql = "DELETE FROM BOOK_DATA WHERE Book_Id = @BookId";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Models.Book_Data> MapBookDataToList(DataTable bookData)
        {
            List<Models.Book_Data> result = new List<Book_Data>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Book_Data()
                {
                    ClassName = row["BookClassName"].ToString(),
                    BookName = row["BookName"].ToString(),
                    BoughtDate = row["BoughtDate"].ToString(),
                    CodeName = row["CodeName"].ToString(),
                    KeeperName = row["KeeperName"].ToString()
                });
            }
            return result;
        }
    }
}