using Book.Model;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Book.Repository.Common;

namespace Book.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        public static List<Author> authors = new List<Author>();

        string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Author
        public async Task<List<Author>> GetAllAuthors()
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
                            tempAuthor.Id = reader.GetGuid(0);
                            tempAuthor.FirstName = reader.GetString(1);
                            tempAuthor.LastName = reader.GetString(2);
                            authors.Add(tempAuthor);
                        }
                    }
                    conn.Close();
                    return authors;
                }
            }
        }

        [HttpGet]
        // GET: api/Author/5
        public async Task<Author> GetAuthorByLastName([FromUri] string lastName)
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
                        while (reader.Read())
                        {
                            tempAuthor.Id = reader.GetGuid(0);
                            tempAuthor.FirstName = reader.GetString(1);
                            tempAuthor.LastName = reader.GetString(2);

                        }
                    }
                    conn.Close();
                    return tempAuthor;
                }
            }
        }

        [HttpPost]
        // POST: api/Values
        public async Task<Author> PostAuthor([FromBody] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("INSERT INTO Author (FirstName, LastName) VALUES (@FirstName, @LastName)", conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", newAuthor.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newAuthor.LastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return newAuthor;
                }
            }
        }

        [HttpPut]
        // PUT: api/Book/PutAuthor
        public async Task<Author> PutAuthor([FromBody] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("UPDATE Author SET FirstName = @FirstName, LastName = @LastName WHERE Id = @AuthorId", conn))
                {
                    cmd.Parameters.AddWithValue("@AuthorId", newAuthor.Id);
                    cmd.Parameters.AddWithValue("@FirstName", newAuthor.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newAuthor.LastName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return newAuthor;
                }
            }
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public async Task DeleteAuthor([FromUri] Author newAuthor)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("DELETE FROM Author WHERE LastName = @LastName", conn))
                {
                    cmd.Parameters.AddWithValue("@LastName", $"{newAuthor.LastName}");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}