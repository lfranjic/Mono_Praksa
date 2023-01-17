using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Book.WebAPI
{
    public class Book
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Id { get; set; }
        public Book() { }

        public Book(string title, string author, string genre, int id)
        {
            this.Title = title;
            this.Author = author;
            this.Genre = genre;
            this.Id = id;
        }
    }
}