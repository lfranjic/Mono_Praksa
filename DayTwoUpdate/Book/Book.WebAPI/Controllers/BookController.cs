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

        SqlConnection connection = new SqlConnection("Data Source = DESKTOP - LHBF9V2\\SQLEXPRESS; Initial Catalog = Praksa; Integrated Security = True");

        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage FindBookById(int id)
        {
            var foundBook = books.Find(product => product.Id == id);
            if (foundBook != null) return Request.CreateResponse(HttpStatusCode.OK, foundBook);
            else return Request.CreateResponse(HttpStatusCode.NotFound, "No such product!");
        }

        [HttpGet]
        // GET: api/Values/5
        public HttpResponseMessage AllBooks()
        {
            if (books != null)
                return Request.CreateResponse(HttpStatusCode.OK, books);
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Empty!");
        }


        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage SaveBook([FromBody] Book newBook)
        {

            if (!books.Exists(product => product.Id == newBook.Id))
            {
                books.Add(newBook);
                return Request.CreateResponse(HttpStatusCode.OK, "Added!");
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest, "Porduct with the same id exists!");
        }


        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage ChangePages([FromUri] int id, [FromUri] int value)
        {
            var foundBook = books.Find(books => books.Id == id);
            foundBook.Pages = value;
            if (foundBook != null)
            {
                if (foundBook.Pages == value) return Request.CreateResponse(HttpStatusCode.OK, "Changed!");
                else return Request.CreateResponse(HttpStatusCode.NotModified, "Error!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Not found!");
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveBook([FromUri] int id)
        {
            int numberOfBooks = books.Count;
            books.Remove(books.Find(books => books.Id == id));
            if (books.Count == numberOfBooks - 1) return Request.CreateResponse(HttpStatusCode.OK, "Deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Error!"); ;
        }
    }
}