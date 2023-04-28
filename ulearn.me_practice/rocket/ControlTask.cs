using System;

namespace func_rocket;

public class ControlTask
{
	private const double dirRatio = 0.25;
	private const double velocityRatio = 0.75;
	private const double alignRatio = 0.96;

	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		var targetVector = target - rocket.Location;
		var normDirection = GetNormalizedAngle(rocket.Direction);
		var normVelocityAngle = GetNormalizedAngle(rocket.Velocity.Angle);
		var newDirection = normDirection * dirRatio + normVelocityAngle * velocityRatio;
		var turn = (newDirection > GetNormalizedAngle(targetVector.Angle) * alignRatio) ? Turn.Left : Turn.Right;
		return turn;
	}

	private static double GetNormalizedAngle(double angle)
    {
		var sign = Math.Sign(angle);
		return sign * (Math.Abs(angle) % (Math.PI * 2));
	}
}