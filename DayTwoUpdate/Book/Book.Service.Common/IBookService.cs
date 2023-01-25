using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.Common;
using Book.Model;
using Book.Model.Common;

namespace Book.Service.Common
{
    public interface IBookService
    {
        Task<List<Model.Book>> GetAllBooks(Paging paging, Sorting sorting, Filtering filtering);
        Task<Model.Book> GetBookById(Guid id);
        Task PostBook(Model.Book book);
        Task PutBook(Model.Book book);
        Task DeleteBook(Model.Book book);
    }
}