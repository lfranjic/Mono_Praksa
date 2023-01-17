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
        [HttpGet]
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
        /*
        public HttpResponseMessage Post([FromBody])
        {
            
        }
        */
        // PUT api/<controller>/5
        /*
        public void Put(int id, [FromBody] )
        {

        }
        */
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}