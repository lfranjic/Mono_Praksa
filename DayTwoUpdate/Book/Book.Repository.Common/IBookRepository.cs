using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Common;
using Book.Model;

namespace Book.Repository.Common
{
    public interface IBookRepository
    {
        Task<List<Model.Book>> GetAllBooks(Paging paging, Sorting sorting, Filtering filtering);
        Task<Model.Book> GetBookById(Guid id);
        Task<Model.Book> PostBook(Model.Book book);
        Task<Model.Book> PutBook(Model.Book book);
        Task DeleteBook(Model.Book book);
    }
}