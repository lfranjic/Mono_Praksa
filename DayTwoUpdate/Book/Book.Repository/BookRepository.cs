using System.Web.Http;
﻿using System;
using System.Net.Http;
using Book.Model;
using Book.Repository.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;

namespace Book.Repository
{
    public class BookRepository : IBookRepository
    {
        public static List<Model.Book> books = new List<Model.Book>();

        public static string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        // GET: api/Values
        public async Task<List<Model.Book>> GetAllBooks()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<Model.Book> books = new List<Model.Book>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Book", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Model.Book tempBook = new Model.Book
                            {
                                Id = reader.GetGuid(0),
                                Title = reader.GetString(1),
                                Pages = reader.GetInt32(2)
                            };
                            books.Add(tempBook);
                        }
                    }
                    conn.Close();
                    return books;
                }
            }
        }

        // GET: api/Values/5
        public async Task<Model.Book> GetBookById([FromUri] Guid id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand
                    ("SELECT * FROM Book WHERE BookId = @BookId", conn))
                {
                    Model.Book tempBook = new Model.Book();
                    cmd.Parameters.AddWithValue("@BookId", id);
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            tempBook.Id = reader.GetGuid(0);
                            tempBook.Title = reader.GetString(1);
                            tempBook.Pages = reader.GetInt32(2);
                        }
                    }
                    conn.Close();
                    return tempBook;
                }
            }
        }

        [HttpPost]
        // POST: api/Values
        public async Task<Model.Book> PostBook([FromBody] Model.Book newBook)
        {
            newBook.Id = Guid.NewGuid();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Book (BookId, Title, Pages) VALUES (@BookId, @Title, @Pages)", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@BookId", newBook.Id);
                    cmd.Parameters.AddWithValue("@Title", newBook.Title);
                    cmd.Parameters.AddWithValue("@Pages", newBook.Pages);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = cmd;
                    await adapter.InsertCommand.ExecuteNonQueryAsync();
                    conn.Close();
                    return newBook;
                }
            }
        }

        [HttpPut]
        // PUT: api/Book/ChangePages
        public async Task<Model.Book> PutBook([FromBody] Model.Book newBook)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Book SET Title = @Title, Pages = @Pages WHERE BookId = @BookId", conn))
                {
                    await conn.OpenAsync();
                    cmd.Parameters.AddWithValue("@BookId", newBook.Id);
                    cmd.Parameters.AddWithValue("@Title", newBook.Title);
                    cmd.Parameters.AddWithValue("@Pages", newBook.Pages);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = cmd;
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                    conn.Close();
                    return newBook;
                }
            }
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public async Task DeleteBook([FromUri] Model.Book newBook)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Book WHERE BookId = @BookId", conn))
                {
                    cmd.Parameters.AddWithValue("@BookId", newBook.Id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    conn.Close();
                }
            }
        }
    }
}