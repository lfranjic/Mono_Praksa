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
        public static List<AuthorRest> authors = new List<AuthorRest>();

        string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage AllAuthors()
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<AuthorRest> authors = new List<AuthorRest>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Author", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AuthorRest tempAuthor = new AuthorRest();
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
        // GET: api/Values/5
        public HttpResponseMessage FindAuthorByLastName([FromUri] string lastName)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("SELECT * FROM Author WHERE LastName=@LastName", conn))
                {
                    AuthorRest tempAuthor = new AuthorRest();
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
        public HttpResponseMessage SaveAuthor([FromBody] AuthorRest newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("INSERT INTO Book (FirstName, LastName) VALUES (@FirstName, @LastName)", conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", newAuthor.AuthorFirstName);
                    cmd.Parameters.AddWithValue("@LastName", newAuthor.AuthorLastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newAuthor);
                }
            }
        }

        [HttpPut]
        // PUT: api/Book/ChangeAuthor
        public HttpResponseMessage ChangeAuthor([FromBody] AuthorRest newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("UPDATE Author SET FirstName = @FirstName, LastName = @LastName WHERE AuthorId = @AuthorId", conn))
                {
                    cmd.Parameters.AddWithValue("@AuthorId", newAuthor.AuthorId);
                    cmd.Parameters.AddWithValue("@FirstName", newAuthor.AuthorFirstName);
                    cmd.Parameters.AddWithValue("@LastName", newAuthor.AuthorLastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, newAuthor);
                }
            }
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveAuthor([FromUri] AuthorRest newAuthor)
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