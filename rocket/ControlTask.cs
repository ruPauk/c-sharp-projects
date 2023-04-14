using System;
using System.Diagnostics;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		var targetVector = target - rocket.Location;
		var currentVector = rocket.Direction * 1 / 3 + rocket.Velocity.Angle * 2 / 3;

		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;
		//var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * (1 - rocket.Velocity.Length/100);

		var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;

		return condition ? Turn.Left : Turn.Right;
	}
}