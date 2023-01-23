﻿using System;
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

namespace Book.WebAPI.Controllers
{
    public class BookController : ApiController
    {
        BookService bookService = new BookService();

        //string connString = "Data Source=DESKTOP-LHBF9V2\\SQLEXPRESS;Initial Catalog=Praksa;Integrated Security=True";

        [HttpGet]
        // GET: api/Values
        public async Task<HttpResponseMessage> AllBooks()
        {
            List<Model.Book> books = await bookService.GetAllBooks();
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
            Model.Book foundBook = await bookService.GetBookById(id);
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
                await bookService.PostBook(newBook);
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
                await bookService.PutBook(newBook);
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
            await bookService.DeleteBook(newBook);
            return Request.CreateResponse(HttpStatusCode.OK, newBook);
        }
    }
}