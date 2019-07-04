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
        public List<Models.BookData> GetBookByCondtioin(Models.BookServiceArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT 
                            bd.BOOK_ID AS BookId,
	                        bc.BOOK_CLASS_NAME AS ClassName,
                            bd.BOOK_NAME AS BookName,
                            FORMAT(bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') AS BoughtDate,
                            bc1.CODE_NAME AS CodeName,
                            ISNULL(mm.USER_ENAME,'') AS KeeperName 
                           FROM BOOK_DATA bd 
	                       JOIN BOOK_CLASS bc
                                ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
	                       JOIN BOOK_CODE bc1 
                                ON bc1.CODE_ID = bd.BOOK_STATUS AND bc1.code_type = 'BOOK_STATUS'
	                       LEFT JOIN MEMBER_M mm 
                                ON bd.BOOK_KEEPER = mm.USER_ID
                           Where (bd.BOOK_NAME LIKE '%'+ @BookName +'%' ) AND
                                 (bc.BOOK_CLASS_ID LIKE '%'+ @ClassName +'%' ) AND
                                 (ISNULL(mm.USER_ID,'') LIKE '%'+ @KeeperName +'%' ) AND
                                 (bc1.CODE_ID LIKE '%'+ @CodeName +'%')
                           ORDER BY bc.BOOK_CLASS_NAME;";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", arg.BookId == null ? string.Empty : arg.BookId));
                cmd.Parameters.Add(new SqlParameter("@ClassName", arg.ClassName == null ? string.Empty : arg.ClassName));
                cmd.Parameters.Add(new SqlParameter("@BookName", arg.BookName == null ? string.Empty : arg.BookName));
                cmd.Parameters.Add(new SqlParameter("@BoughtDate", arg.BoughtDate == null ? string.Empty : arg.BoughtDate));
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
        public void DeleteBookById(string bookId)
        {
            try
            {
                string sql = "DELETE FROM BOOK_DATA WHERE BOOK_ID = @BookId ";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BookId", bookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        /// <summary>
        /// 新增書籍資料
        /// </summary>
        public void InsertBook(Models.BookData bookdata)
        {
            string sql = @" INSERT INTO BOOK_DATA
                         (
                            BOOK_NAME,BOOK_CLASS_ID,BOOK_AUTHOR,BOOK_BOUGHT_DATE,BOOK_PUBLISHER,
					        BOOK_NOTE,BOOK_STATUS
                          )
		                 VALUES
                          (
                            @BOOK_NAME,@BOOK_CLASS_ID,@BOOK_AUTHOR,
                            @BOOK_BOUGHT_DATE,@BOOK_PUBLISHER,@BOOK_NOTE,'A'
                           )";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", bookdata.BookName));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", bookdata.ClassId));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", bookdata.BookAuthor));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", bookdata.Publisher));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", bookdata.BoughtDate));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", bookdata.BookNote));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        private List<Models.BookData> MapBookDataToList(DataTable bookData)
        {
            List<Models.BookData> result = new List<BookData>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new BookData()
                {
                    BookId = (int)row["BookId"],
                    ClassName = row["ClassName"].ToString(),
                    BookName = row["BookName"].ToString(),
                    BoughtDate = row["BoughtDate"].ToString(),
                    CodeName = row["CodeName"].ToString(),
                    KeeperName = row["KeeperName"].ToString()
                });
            }
            return result;
        }

        /// <summary>
        /// 調閱紀錄
        /// </summary>
        public List<Models.LendData> LendBookById(int bookId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
                            FORMAT(blr.LEND_DATE,'yyyy/mm/dd') AS LendDate,
                            blr.KEEPER_ID AS KeeperId,
                            mm.USER_ENAME AS UserEname,
                            mm.USER_CNAME AS UserCname
                            FROM 
                            BOOK_LEND_RECORD blr JOIN MEMBER_M mm
                            ON blr.KEEPER_ID = mm.USER_ID
                            WHERE blr.BOOK_ID = @BookId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", bookId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapRecordDataToList(dt);
        }

        private List<LendData> MapRecordDataToList(DataTable dt)
        {
            List<Models.LendData> result = new List<LendData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new LendData()
                {
                    LendDate = row["LendDate"].ToString(),
                    KeeperId = row["KeeperId"].ToString(),
                    KeeperName = row["UserEname"].ToString(),
                    KeeperCName = row["UserCname"].ToString()


                });
            }
            return result;
        }
    }
}