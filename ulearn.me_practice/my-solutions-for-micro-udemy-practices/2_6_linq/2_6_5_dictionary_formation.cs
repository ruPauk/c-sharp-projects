using System;

//TASK - LINQ practicing - OrderBy/Distinct/ThenBy
https://ulearn.me/course/basicprogramming2/Sostavlenie_slovarya_acb110b3-c2f0-4e1a-9645-76df88a75a7f

namespace Tests
{
    class Program
    {
        public static void Main()
		{
			var vocabulary = GetSortedWords(
				"Hello, hello, hello, how low",
				"",
				"With the lights out, it's less dangerous",
				"Here we are now; entertain us",
				"I feel stupid and contagious",
				"Here we are now; entertain us",
				"A mulatto, an albino, a mosquito, my libido...",
				"Yeah, hey"
			);
			foreach (var word in vocabulary)
				Console.WriteLine(word);
		}

		public static string[] GetSortedWords(params string[] textLines)
		{
			return textLines
				.SelectMany(x => x.Split(' ', ',', '\'', '.', ';')
					.Where(y => (y != "" && y != " "))
					.Select(y => y.ToLower()))
				.Distinct()
				.OrderBy(x => x)
				.ToArray();
		}
    }
}