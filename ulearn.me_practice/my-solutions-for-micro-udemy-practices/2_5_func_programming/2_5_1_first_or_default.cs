using System;

//TASK - LINQ practicing - FirstOrDefault
//https://ulearn.me/course/basicprogramming2/Realizatsiya_metoda_FirstOrDefault_e3d4c78d-8d68-40cb-8b1e-a1912396dbee

namespace Tests
{
    class Program
    {
        public static void Main()
		{
			Assert.AreEqual(0, FirstOrDefault(new int[0], x => true)); // default(int) == 0
			Assert.AreEqual(null, FirstOrDefault(new string[0], x => true)); // default(string) == null
			Assert.AreEqual(3, FirstOrDefault(new[] { 1, 2, 3 }, x => x > 2));
			Assert.AreEqual(3, FirstOrDefault(new[] { 3, 2, 1 }, x => x > 2));
			Assert.AreEqual(3, FirstOrDefault(new[] { 2, 3, 1 }, x => x > 2));
			CheckYieldReturn();

			Console.WriteLine("OK");
		}

        private static T FirstOrDefault<T>(IEnumerable<T> source, Func<T, bool> filter)
		{
			foreach(var elem in source)
				if (filter(elem))
					return elem;
			return default(T);
		}
    }
}