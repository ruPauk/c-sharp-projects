using System;
using System.Diagnostics;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		
		var targetVector = target - rocket.Location;
		var currentVector = rocket.Direction * 1 / 3 + rocket.Velocity.Angle * 2 / 3;

		var rocketVector = 
		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;
		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * (1 - rocket.Velocity.Length/100);

		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;
		//var condition = GetNormalizedAngle(rocket.Direction) > GetNormalizedAngle(targetVector.Angle) * 0.75;

		var scalarValue = GetScalarMultiplication(targetVector, currentVector);
		if (Math.Abs(scalarValue) > 0.98f)
			return Turn.None;
		var condition = scalarValue < 0 ? Turn.Left : Turn.Right;
		

		Debug.WriteLine($"N = {GetScalarMultiplication(targetVector, rocket.Velocity)}, Dir = {rocket.Direction}, V = {rocket.Velocity}, tVAngle = {targetVector.Angle}, ND = {GetNormalizedAngle(rocket.Direction)}");
		//return condition ? Turn.Left : Turn.Right;
		//return condition;
		return Turn.None;
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
		return new Vector(x, y);
    }
}