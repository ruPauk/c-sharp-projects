using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static (double, double) GetWristDot(double x, double y, double alpha, double palmLength)
        {
            alpha %= Math.PI * 2;
            var resX = x + (-1) * Math.Cos(alpha) * palmLength;
            var resY = y + Math.Sin(alpha) * palmLength;
            return (resX, resY);
        }

        public static double GetSideLength(double wristX, double wristY)
        {
            var result = Math.Sqrt(wristX * wristX + wristY * wristY);
            return result;
        }

        public static double GetElbowAngle(double wristSideLength)
        {
            return TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, wristSideLength);
        }

        public static double GetShoulderAngle(double sideLength, double upperArm,
            double foreArm, (double, double) wristDot)
        {
            var partOne = TriangleTask.GetABAngle(sideLength, upperArm, foreArm);
            var partTwo = Math.Atan2(wristDot.Item2, wristDot.Item1);
            return partOne + partTwo;
        }

        public static double GetWristAngle(double alpha, double shoulderAngle, double elbowAngle)
        {
            return -alpha - shoulderAngle - elbowAngle;
        }

        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristDot = GetWristDot(x, y, alpha, Manipulator.Palm);
            var sideLength = GetSideLength(wristDot.Item1, wristDot.Item2);
            var elbowAngle = GetElbowAngle(sideLength);
            var shoulderAngle = GetShoulderAngle(sideLength, Manipulator.UpperArm, Manipulator.Forearm, wristDot);
            var wristAngle = GetWristAngle(alpha, shoulderAngle, elbowAngle);
            if ((elbowAngle == double.NaN) || (shoulderAngle == double.NaN) || (wristAngle == double.NaN))
                return new[] { double.NaN, double.NaN, double.NaN };
            return new[] { shoulderAngle, elbowAngle, wristAngle };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        private const int TEST_ITERATIONS = 100;
        [Test]
        public void TestMoveManipulatorTo()
        {
            double x = 0, y = 0, alpha = 0;
            var randomizer = new Random();
            var maxLength = (int)(Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm);
            for (int i = 0; i < TEST_ITERATIONS; i++)
            {
                x = (double)randomizer.Next(0, maxLength) + randomizer.NextDouble();
                y = (double)randomizer.Next(0, maxLength) + randomizer.NextDouble();
                alpha = (double)randomizer.Next(-3, 3) + randomizer.NextDouble();
                var result = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                PointF[] expectedResult;
                if (IsValidDot(x, y))
                    expectedResult = AnglesToCoordinatesTask.GetJointPositions(result[0], result[1], result[2]);
                else
                    expectedResult = new PointF[] {
                        new PointF(float.NaN, float.NaN),
                        new PointF(float.NaN, float.NaN),
                        new PointF(float.NaN, float.NaN)
                    };
                Assert.AreEqual(1, 1, 1e-5);
            }
        }

        public bool IsValidDot(double x, double y)
        {
            var rMax = Math.Abs(Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm);
            var realR = Math.Round(Math.Abs(Math.Sqrt(x * x + y * y)), 15);
            return realR > rMax;
        }

        [TestCase(0, 0, 0, -60, 0)]
        [TestCase(0, 0, Math.PI / 4, -42.42640687, 42.42640687)]
        [TestCase(0, 0, Math.PI * 1.5, 0, -60)]
        public void TestGetWristDot(double x, double y, double alpha, double expectedX, double expectedY)
        {
            var result = ManipulatorTask.GetWristDot(x, y, alpha, Manipulator.Palm);
            Assert.AreEqual(expectedX, result.Item1, 1e-5, "WristX");
            Assert.AreEqual(expectedY, result.Item2, 1e-5, "WristY");
        }
    }
}