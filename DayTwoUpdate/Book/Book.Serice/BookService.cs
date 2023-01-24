using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Model;
using Book.Repository;
using Book.Repository.Common;
using Book.Service.Common;

namespace Book.Service
{
    public class BookService : IBookService
    {
        //public BookRepository repository = new BookRepository();
        protected IBookRepository repository { get; set; }
        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }
        public BookService()
        {

        }

        public async Task<List<Model.Book>> GetAllBooks()
        {
            List<Model.Book> models = await repository.GetAllBooks();
            return models;
        }

        public async Task<Model.Book> GetBookById(Guid id)
        {
            Model.Book foundBook = await repository.GetBookById(id);

            if (foundBook != null)
            {
                return foundBook;
            }
            return null;
        }

        public async Task PostBook(Model.Book book)
        {
            await repository.PostBook(book);
        }

        public async Task PutBook(Model.Book book)
        {
            await repository.PutBook(book);
        }

        public async Task DeleteBook(Model.Book book)
        {
            await repository.DeleteBook(book);
        }
    }
}