using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.WebAPI.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace Book.WebAPI.Controllers
{
    public class BookController : ApiController
    {
        public static List<Book> books = new List<Book>
        {
            /*
            new Book(4, "The Lord of the Rings", 750),
            new Book(5, "Crime and Punishment", 500),
            new Book(6, "The Hobbit", 240)
            */
        };

        string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage AllBooks()
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<Book> books = new List<Book>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Book tempBook = new Book();
                            tempBook.Id = reader.GetGuid(0);
                            tempBook.Title = reader.GetString(1);
                            tempBook.Pages = reader.GetInt32(2);
                            books.Add(tempBook);
                        }
                    }
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, books);
                }
            }
        }

        [HttpGet]
        // GET: api/Values/5
        public HttpResponseMessage FindBookByTitle([FromUri] string title)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("SELECT * FROM Book WHERE Title=@Title", conn))
                {
                    Book tempBook = new Book();
                    cmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                tempBook.Id = reader.GetGuid(0);
                                tempBook.Title = reader.GetString(1);
                                tempBook.Pages = reader.GetInt32(2);
                            }
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                       
                    }
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, tempBook);
                }
            }
        }

        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage SaveBook([FromBody] Book newBook)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using(SqlCommand cmd = new SqlCommand
                    ("INSERT INTO Book (Title, Pages) VALUES (@Title, @Pages)", conn))
                {
                    cmd.Parameters.AddWithValue("@Title", newBook.Title);
                    cmd.Parameters.AddWithValue("@Pages", newBook.Pages);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newBook);
                }
            }
        }

        [HttpPut]
        // PUT: api/Book/ChangePages
        public HttpResponseMessage ChangePages([FromBody] Book newBook)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using(SqlCommand cmd = new SqlCommand
                    ("UPDATE Book SET Title = @Title, Pages = @Pages WHERE BookId = @BookId", conn))
                {
                    cmd.Parameters.AddWithValue("@BookId", newBook.Id);
                    cmd.Parameters.AddWithValue("@Title", newBook.Title);
                    cmd.Parameters.AddWithValue("@Pages", newBook.Pages);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newBook);
                }
            }
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveBook([FromUri] Book newBook)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using(SqlCommand cmd = new SqlCommand
                    ("DELETE FROM Book WHERE Title = @Title", conn))
                {
                    cmd.Parameters.AddWithValue("@Title", $"{newBook.Title}");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted.");
                }
            }
        }
    }
}