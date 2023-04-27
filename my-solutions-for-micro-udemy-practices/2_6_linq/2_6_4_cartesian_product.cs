using System;

//TASK - LINQ practicing
//https://ulearn.me/course/basicprogramming2/Dekartovo_proizvedenie_ff3215d3-5cc7-4c28-83b1-77465f570dc8

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            var p = new Point(-1, 3);
            Console.WriteLine($"Point [{p.X}, {p.Y}]");
            var points = GetNeighbours(p);
            foreach (var point in points)
            {
                Console.WriteLine(point);
            }
        }

        public static IEnumerable<Point> GetNeighbours(Point p)
        {
            int[] d = { -1, 0, 1 };
            return d.SelectMany(x => d
                                .Select(y => new Point(x, y))
                                .Select(x => new Point(x.X + p.X, x.Y + p.Y)))
                                .Where(x => !x.Equals(p));
        }
    }
}