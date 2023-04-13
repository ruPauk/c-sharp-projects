using System;
using System.Diagnostics;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		var targetVector = target - rocket.Location;
		var condition = rocket.Direction % (Math.PI * 2) > (targetVector.Angle) % (Math.PI * 2) * 0.75;

		Debug.WriteLine("Dir - " + rocket.Direction + "targetVector.Angle = " + targetVector.Angle + " ~ " + (condition ? "Turn.Left" : "Turn.Right"));

		//return Turn.None;

		return condition ? Turn.Left : Turn.Right;
	}
}