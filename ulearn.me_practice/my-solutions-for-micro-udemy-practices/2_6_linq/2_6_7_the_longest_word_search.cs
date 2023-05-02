using System;

//TASK - LINQ practicing - aggregation
//https://ulearn.me/course/basicprogramming2/Poisk_samogo_dlinnogo_slova_04a35ae5-67b9-4674-b4c5-98e8976f87f9

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine(GetLongest(new[] { "azaz", "as", "sdsd" }));
            Console.WriteLine(GetLongest(new[] { "zzzz", "as", "sdsd" }));
            Console.WriteLine(GetLongest(new[] { "as", "12345", "as", "sds" }));
        }

        public static string? GetLongest(IEnumerable<string> words)
        {
            return words
                .Select(x => (x, x.Length))
                .Where(y => y.Length == words.Max(a => a.Length))
                .Min(x => x.Item1);
        }
    }
}