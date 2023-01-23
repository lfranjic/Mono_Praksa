using Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Common
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorByLastName(string title);
        Task<Author> PostAuthor(Author author);
        Task<Author> PutAuthor(Author author);
        Task DeleteAuthor(Author author);
    }
}