using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Book.WebAPI.Controllers
{

    public class BookController : ApiController
    {
        public static List<Book> books = new List<Book>
        {
            new Book("The Hobbit", "J.R.R. Tolkien", "Fantasy", 1),
            new Book("Crime and Punishment", "Fjodor Dostoevsky", "Psychological", 2),
            new Book("Harry Potter", "J.K. Rowling", "Fantasy", 3)
        };

        //Get api controller
        public HttpResponseMessage GetBooks()
        {
            if (books == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, books);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET api/<controller>/5
        [HttpGet]
        public HttpResponseMessage GetBooksById(int id)
        {
            var foundBook = books.Find(books => books.Id == id);
            if (foundBook == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, foundBook);
            }
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        // POST api/<controller>
        // POST: api/Values
        public HttpResponseMessage saveBook([FromBody] Book newBook)
        {

            if (!books.Exists(Book => Book.Id == newBook.Id))
            {
                books.Add(newBook);
                return Request.CreateResponse(HttpStatusCode.OK, "Added!");
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest, "Book with the same id exists!");
        }

        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage changeGenre([FromUri] int id, [FromUri] string value)
        {
            var foundBook = books.Find(Book => Book.Id == id);
            foundBook.Genre = value;
            if (foundBook != null)
            {
                if (foundBook.Genre == value) return Request.CreateResponse(HttpStatusCode.OK, "Changed!");
                else return Request.CreateResponse(HttpStatusCode.NotModified, "Error!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Not found!");
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage removeBook([FromUri] int id)
        {
            int numberOfbooks = books.Count;
            books.Remove(books.Find(Book => Book.Id == id));
            if (books.Count == numberOfbooks - 1) return Request.CreateResponse(HttpStatusCode.OK, "Deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotModified, "Error!"); ;
        }
    }
}
