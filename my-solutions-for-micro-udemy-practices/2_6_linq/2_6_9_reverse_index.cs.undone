using System;

//TASK - LINQ practicing
//https://ulearn.me/course/basicprogramming2/Ob_edinenie_kollektsiy_7db3f797-b99b-4580-abe6-bb4ee929bb6b

namespace Tests
{
    class Program
    {
         public static void Main()
        {
            Classroom[] classes =
            {
                new Classroom {Students = {"Pavel", "Ivan", "Petr"},},
                new Classroom {Students = {"Anna", "Ilya", "Vladimir"},},
                new Classroom {Students = {"Bulat", "Alex", "Galina"},}
            };
            var allStudents = GetAllStudents(classes);
            Array.Sort(allStudents);
            Console.WriteLine(string.Join(" ", allStudents));
        }

        public class Classroom
        {
            public List<string> Students = new List<string>();
        }

        public static string[] GetAllStudents(Classroom[] classes)
        {
            return classes
                .SelectMany(x => x.Students.ToArray())
                .ToArray();
        }
    }
}