using System;
using System.Collections.Generic;
using System.Text;

namespace DayOne
{
    public class Novel : Book
    {
        private string v1;
        private string v2;
        private DateTime dateTime;
        private bool v3;

        public string Title { get; set; }
        public string Author { get; set; }
        private string Text { get; set; }
        public DateTime Published { get; set; }

        public Novel(
            string Title,
            string Author,
            string Text,
            DateTime Published)
        {
            this.Title = Title;
            this.Author = Author;
            this.Text = Text;
            this.Published = Published;
        }

        public Novel(string v1, string v2, DateTime dateTime)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.dateTime = dateTime;
        }

        public override string ToString => String.Format(
                "Title: {0}\n" +
                "Author: {1}\n" +
                "Text: {2}\n" +
                "First publish: {3}",
                Title,
                Author,
                Text,
                Published.Year);
    }
}
