using System;

//TASK - LINQ practicing - tuples sort
//https://ulearn.me/course/basicprogramming2/Sortirovka_kortezhey_80d43879-1099-4972-aee1-6eb3edf1e923

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            string s = "Бык тупогуб, тупогубенький бычок, у быка губа бела была тупа";
            var sortedWords = GetSortedWords(s);
            foreach (var str in sortedWords)
                Console.WriteLine(str);
        }

        public static List<string> GetSortedWords(string text)
        {
            return Regex.Split(text, @"\W+")
                    .Where(x => x != "")
                    .Select(x => x.ToLower())
                    .OrderBy(x => (x.Length, x))
                    .Distinct()
                    .ToList();
        }
    }
}