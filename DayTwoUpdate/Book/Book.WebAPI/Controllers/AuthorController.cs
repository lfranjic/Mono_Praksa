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
    public class AuthorController : ApiController
    {
        public static List<Author> authors = new List<Author>
        {
            /*
            new Book(4, "The Lord of the Rings", 750),
            new Book(5, "Crime and Punishment", 500),
            new Book(6, "The Hobbit", 240)
            */
        };

        string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Author
        public HttpResponseMessage AllAuthors()
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<Author> authors = new List<Author>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Author", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Author tempAuthor = new Author();
                            tempAuthor.AuthorId = reader.GetGuid(0);
                            tempAuthor.AuthorFirstName = reader.GetString(1);
                            tempAuthor.AuthorLastName = reader.GetString(2);
                            authors.Add(tempAuthor);
                        }
                    }
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, authors);
                }
            }
        }

        [HttpGet]
        // GET: api/Author/5
        public HttpResponseMessage FindAuthorByLastName([FromUri] string lastName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("SELECT * FROM Author WHERE LastName=@LastName", conn))
                {
                    Author tempAuthor = new Author();
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                tempAuthor.AuthorId = reader.GetGuid(0);
                                tempAuthor.AuthorFirstName = reader.GetString(1);
                                tempAuthor.AuthorLastName = reader.GetString(2);
                            }
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }

                    }
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, tempAuthor);
                }
            }
        }

        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage SaveAuthor([FromBody] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("INSERT INTO Author (FirstName, LastName) VALUES (@AuthorFirstName, @AuthorLastName)", conn))
                {
                    cmd.Parameters.AddWithValue("@AuthorFirstName", newAuthor.AuthorFirstName);
                    cmd.Parameters.AddWithValue("@AuthorLastName", newAuthor.AuthorLastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newAuthor);
                }
            }
        }

        [HttpPut]
        // PUT: api/Book/ChangeAuthor
        public HttpResponseMessage ChangeAuthor([FromBody] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("UPDATE Author SET FirstName = @AuthorFirstName, LastName = @AuthorLastName WHERE AuthorId = @AuthorId", conn))
                {
                    cmd.Parameters.AddWithValue("@AuthorId", newAuthor.AuthorId);
                    cmd.Parameters.AddWithValue("@AuthorFirstName", newAuthor.AuthorFirstName);
                    cmd.Parameters.AddWithValue("@AuthorLastName", newAuthor.AuthorLastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newAuthor);
                }
            }
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveAuthor([FromUri] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("DELETE FROM Author WHERE LastName = @LastName", conn))
                {
                    cmd.Parameters.AddWithValue("@LastName", $"{newAuthor.AuthorLastName}");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted.");
                }
            }
        }
    }
}
