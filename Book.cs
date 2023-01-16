using System;
using System.Collections.Generic;
using System.Text;

namespace DayOne
{
    public abstract class Book: IBook
    {
        string IBook.ToString => throw new NotImplementedException();
        public new abstract string ToString { get; }
    }
}
