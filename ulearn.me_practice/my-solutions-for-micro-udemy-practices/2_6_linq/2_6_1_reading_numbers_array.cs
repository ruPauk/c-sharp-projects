using System;

//TASK - LINQ practicing
//https://ulearn.me/course/basicprogramming2/Chtenie_massiva_chisel_cba7bc68-f1b9-46b1-93d4-49ac113a1d02

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            foreach (var num in ParseNumbers(new[] { "-0", "+0000" }))
                Console.WriteLine(num);
            foreach (var num in ParseNumbers(new List<string> { "1", "", "-03", "0" }))
                Console.WriteLine(num);
        }

        public static int[] ParseNumbers(IEnumerable<string> lines)
        {
            return lines
                .Where(line => line != "")
                .Select(line => Convert.ToInt32(line))
                .ToArray();
        }
    }
}