using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }

        public Book(Guid id, string title, int pages)
        {
            Id = id;
            Title = title;
            Pages = pages;
        }

        public Book()
        {

        }
    }
}