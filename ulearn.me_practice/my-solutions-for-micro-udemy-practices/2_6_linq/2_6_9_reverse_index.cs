using System;
using System.Text.RegularExpressions;

//TASK - LINQ practicing - dictionary and lookup
//https://ulearn.me/course/basicprogramming2/Sozdanie_obratnogo_indeksa_52caf978-4bb7-4cc1-92fb-607153da0a1e

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            Document[] documents =
            {
                new Document {Id = 1, Text = "Hello world!"},
                new Document {Id = 2, Text = "World, world, world... Just words..."},
                new Document {Id = 3, Text = "Words — power"},
                new Document {Id = 4, Text = ""}
            };

            var index = BuildInvertedIndex(documents);
            SearchQuery("world", index);
            SearchQuery("words", index);
            SearchQuery("power", index);
            SearchQuery("cthulhu", index);
            SearchQuery("", index);
        }

        public class Document
        {
            public int Id;
            public string Text;
        }

        public static ILookup<string, int> BuildInvertedIndex(Document[] documents)
        {
            return documents
                .SelectMany(doc => Regex.Split(doc.Text.ToLower(), @"\W+")
                    .Where(x => x != "")
                    .Distinct()
                    .Select(word => (word, doc.Id)))
                .ToLookup(str => str.word, str => str.Id);
        }

        public static void SearchQuery(string str, ILookup<string, int> index)
        {
            Console.Write($"KEY - {str} ->");
            foreach (var i in index[str])
            {
                Console.Write($" {i}");
            }
            Console.WriteLine();
        }
    }
}