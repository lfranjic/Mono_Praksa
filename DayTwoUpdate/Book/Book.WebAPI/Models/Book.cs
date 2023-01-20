using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data.SqlClient;

namespace Book.WebAPI
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public Book() { }

        public Book(int bookId, string title, int pages)
        {
            Id = bookId;
            Title = title;
            Pages = pages;
        }
    }
}