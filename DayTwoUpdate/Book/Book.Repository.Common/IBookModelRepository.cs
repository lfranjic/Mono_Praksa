using Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Common
{
    public interface IBookModelRepositoryCommon
    {
        Task<List<Model.Book>> GetAllBooks();
        Task<Model.Book> GetBookByTitle(string title);
        Task<Model.Book> PostBook(Model.Book book);
        Task<Model.Book> PutBook(Model.Book book);
        Task DeleteBook(Model.Book book);
    }
}