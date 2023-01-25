using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Book.WebAPI.Models;
using System.Threading.Tasks;
using System.Text;
using Book.Service;
using Book.Model;
using Book.Model.Common;
using Book.Service.Common;
using Book.Common;

namespace Book.WebAPI.Controllers
{
    public class BookController : ApiController
    {
        //BookService bookService = new BookService();

        private IBookService service { get; set; }
        public BookController(IBookService service)
        {
            this.service = service;
        }
        public BookController()
        {

        }
        //string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Values
        public async Task<HttpResponseMessage> AllBooks(int pageNumber = 1, int pageSize = 2, string sortBy = "Title", string sortOrder = "asc", int? pageMin = null, int? pageMax = null, string bookTitle=null)
        {
            Paging paging = new Paging(pageSize, pageNumber);
            Sorting sorting = new Sorting(sortBy, sortOrder);
            Filtering filtering = new Filtering(pageMin, pageMax, bookTitle);
            List<Model.Book> books = await service.GetAllBooks(paging, sorting, filtering);
            if (books != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, books);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty.");
        }

        [HttpGet]
        // GET: api/Values/5
        public async Task<HttpResponseMessage> GetBookById(Guid id)
        {
            Model.Book foundBook = await service.GetBookById(id);
            if (foundBook != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, foundBook);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No such book.");
        }

        [HttpPost]
        // POST: api/Values
        public async Task<HttpResponseMessage> PostBook([FromBody] Model.Book newBook)
        {
            newBook.Id = Guid.NewGuid();
            if (newBook != null)
            {
                await service.PostBook(newBook);
                return Request.CreateResponse(HttpStatusCode.OK, newBook);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Error!");
        }

        [HttpPut]
        // PUT: api/Book/ChangePages
        public async Task<HttpResponseMessage> PutBook([FromBody] Model.Book newBook)
        {
            if (newBook != null)
            {
                await service.PutBook(newBook);
                return Request.CreateResponse(HttpStatusCode.NotFound, newBook);
            }
            return Request.CreateResponse(HttpStatusCode.OK, newBook);
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public async Task<HttpResponseMessage> RemoveBook([FromUri] Model.Book newBook)
        {
            if (newBook == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            await service.DeleteBook(newBook);
            return Request.CreateResponse(HttpStatusCode.OK, newBook);
        }
    }
}