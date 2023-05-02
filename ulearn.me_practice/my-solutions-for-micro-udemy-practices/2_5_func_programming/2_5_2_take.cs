using System;

//TASK - LINQ practicing - Take
https://ulearn.me/course/basicprogramming2/Realizatsiya_metoda_Take_e3d4c70d-0d68-40cb-8b1e-a1912396dbee

namespace Tests
{
    class Program
    {
        public static void Main()
		{
			Func<int[], int, string> take = (source, count) => string.Join(" ", Take(source, count));

			Assert.AreEqual("1 2", take(new[] { 1, 2, 3, 4 }, 2));
			Assert.AreEqual("4", take(new[] { 4 }, 1));
			Assert.AreEqual("", take(new[] { 5 }, 0));

			var num = new Random().Next(0, 1000);
			Assert.AreEqual(num.ToString(), take(new[] { num }, 100500));

			CheckLazyness();
			Console.WriteLine("OK");
		}

        private static IEnumerable<T> Take<T>(IEnumerable<T> source, int count)
		{
			if (count <= 0)
				yield break;
			foreach(var elem in source)
			{
				yield return elem;
				count--;
				if (count == 0)
					yield break;	
			}
		}
    }
}