using System;
using System.Diagnostics;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		var targetVector = target - rocket.Location;
		var rocketVector = GetVector(rocket.Location, rocket.Direction);
		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;
		var dirNorm = GetNormalizedAngle(rocket.Direction);
		var tVNorm = GetNormalizedAngle(targetVector.Angle);
		var condition =  dirNorm > tVNorm * 0.75;
		var res = condition ? Turn.Left : Turn.Right;
		res = (Math.Abs(Math.Abs(dirNorm) - Math.Abs(tVNorm)) < 0.05) ? Turn.None : res;
		Debug.WriteLine($"Dir - {GetNormalizedAngle(rocket.Direction)}, tVA - {GetNormalizedAngle(targetVector.Angle)}, con - {res}, V - {rocket.Velocity}");
		return res;
	}

	private static double GetNormalizedAngle(double angle)
    {
		var sign = Math.Sign(angle);
		//return sign * (Math.Abs(angle) % (Math.PI * 2));
		return sign * (Math.Abs(angle) % (Math.PI));
	}

	private static double GetScalarMultiplication(Vector a, Vector b)
    {
		a = a.Normalize();
		b = b.Normalize();

		return a.X * b.X + a.Y * b.Y;
    }

	private static Vector GetVector(Vector location, double direction)
    {
		var x = location.X * Math.Cos(direction);
		var y = location.Y * Math.Sin(direction);
		return new Vector(x, y).Normalize();
    }
}