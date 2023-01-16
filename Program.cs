using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Novel novel = new Novel(
                "The Fellowship of the Ring", 
                "J.R.R. Tolkien", 
                "", 
                new DateTime(1954, 1, 1));
            Console.WriteLine(
                novel.Title +
                "\n" + novel.Author +
                "\n" + novel.Published.Year);

            Console.WriteLine();

            GenericsList<int> list = new GenericsList<int>();

            for (int x = 10; x > 0; x--)
            {
                list.AddHead(x);
            }

            foreach (int i in list)
            {
                System.Console.Write("Chapter " + i + "\n");
            }

            Console.WriteLine();

            int[] yearsOfPublishedBooks = {
                1937, 1954, 1977,
                1980, 2017, 2007,
                2022, 2018, 1962};

            //Može se koristiti "var" umjesto Generic tipa
            IEnumerable<int> yearQuery =
                from years in yearsOfPublishedBooks
                where years > 2000
                select years;
            foreach (int i in yearQuery)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
