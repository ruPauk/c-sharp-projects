using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
        /// </summary>
        public static double GetABAngle(double a, double b, double c)
        {
            if ((a + b >= c) && (a + c >= b) && (b + c >= a))
            {
                var cosY = (b * b + a * a - c * c) / (2 * a * b);
                return Math.Acos(cosY);
            }
            else
                return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(2, 1, 3, Math.PI)]
        [TestCase(3.0d, 3.0d, 3.0d, 1.0471975511966d)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var result = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, result, 1e-9);
        }
    }
}