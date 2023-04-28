using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = GetLimbEndPosition(0, 0, Manipulator.UpperArm, shoulder);
            var wristPos = GetLimbEndPosition(elbowPos.X, elbowPos.Y, Manipulator.Forearm,
                shoulder + Math.PI + elbow);
            var palmEndPos = GetLimbEndPosition(wristPos.X, wristPos.Y,
                Manipulator.Palm, shoulder + 2 * Math.PI + elbow + wrist);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        public static PointF GetLimbEndPosition(
            float basisX,
            float basisY,
            float partLength,
            double partAngle)
        {
            var x = (partLength * (float)Math.Round(Math.Cos(partAngle), 15));
            var y = (partLength * (float)Math.Round(Math.Sin(partAngle), 15));
            PointF position = new PointF(x, y);
            position.X = position.X + basisX;
            position.Y = position.Y + basisY;
            return position;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        const float SQRT2 = 1.414213562373095f;
        const float SQRT3 = 1.732050807568877f;

        [TestCase(Math.PI / 2, 0, 3 * Math.PI / 2, Manipulator.Palm, Manipulator.UpperArm - Manipulator.Forearm)]
        [TestCase(Math.PI, Math.PI / 2, Math.PI / 2, -Manipulator.UpperArm + Manipulator.Palm, Manipulator.Forearm)]
        [TestCase(3 * Math.PI / 2, Math.PI / 2, 3 * Math.PI / 4, -Manipulator.Forearm - Manipulator.Palm * SQRT2 / 2,
            -Manipulator.UpperArm + Manipulator.Palm * SQRT2 / 2)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist,
            double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Math.Round(Math.Sqrt(Math.Pow(joints[0].X, 2) + Math.Pow(joints[0].Y, 2)), 15),
                (float)Manipulator.UpperArm, 1e-5, "UpperArmCheck");
            Assert.AreEqual(Math.Round(Math.Sqrt(Math.Pow(joints[1].X - joints[0].X, 2) +
                Math.Pow(joints[1].Y - joints[0].Y, 2)), 15), (float)Manipulator.Forearm, 1e-5, "ForeArmCheck");
            Assert.AreEqual(Math.Round(Math.Sqrt(Math.Pow(joints[2].X - joints[1].X, 2) +
                Math.Pow(joints[2].Y - joints[1].Y, 2)), 15), (float)Manipulator.Palm, 1e-5, "PalmCheck");
        }

        [TestCase(0, 0, 4, Math.PI / 4, 2 * SQRT2, 2 * SQRT2)]
        [TestCase(0, 0, 4, Math.PI / 2, 0, 4)]
        [TestCase(2 * SQRT2, 2 * SQRT2, 10, 2 * Math.PI / 4 + Math.PI, 2 * SQRT2, 2 * SQRT2 - 10)]
        [TestCase(2 * SQRT2, 2 * SQRT2, 10, -Math.PI, 2 * SQRT2 - 10, 2 * SQRT2)]
        public void TestGetPosition(float basisX, float basisY, float partLength, double partAngle,
                                    double resultX, double resultY)
        {
            var position = AnglesToCoordinatesTask.GetLimbEndPosition(basisX, basisY, partLength, partAngle);
            Assert.AreEqual(resultX, position.X, 1e-5, "palm endX");
            Assert.AreEqual(resultY, position.Y, 1e-5, "palm endY");
        }
    }
}