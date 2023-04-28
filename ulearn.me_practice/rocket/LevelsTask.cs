using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new Physics();
	static readonly Vector targetConst = new Vector(600, 200);
	static readonly Vector initialRocketLocationConst = new Vector(200, 500);

	public static IEnumerable<Level> CreateLevels()
	{
		yield return GenerateLevel("Zero", (size, v) => Vector.Zero, targetConst);
		yield return GenerateLevel("Heavy", (size, v) => new Vector(0, 0.9), targetConst);
		yield return GenerateLevel("Up", (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300.0)), new Vector(700, 500));

		Gravity whiteHoleGravityDelegate = (size, v) => WhiteHoleGravity(v, targetConst);
		yield return GenerateLevel("WhiteHole", whiteHoleGravityDelegate, targetConst);

		Gravity blackHoleGravityDelegate = (size, v) => BlackHoleGravity(v, targetConst, initialRocketLocationConst);
		yield return GenerateLevel("BlackHole", blackHoleGravityDelegate, targetConst);

		Gravity blackAndWhiteDelegate = (size, v) => 
			new Vector(whiteHoleGravityDelegate(size, v).X + blackHoleGravityDelegate(size, v).X,
			whiteHoleGravityDelegate(size, v).Y + blackHoleGravityDelegate(size, v).Y);
		yield return GenerateLevel("BlackAndWhite", (size, v) => new Vector(blackAndWhiteDelegate(size, v).X / 2,
			blackAndWhiteDelegate(size, v).Y / 2), targetConst);
	}

	private static Vector WhiteHoleGravity(Vector v, Vector target)
	{
		var d = (target - v).Length;
		var normalizedVector = (target - v).Normalize();
		return  normalizedVector * (-140 * d) / (d * d + 1);
	}

	private static Vector BlackHoleGravity(Vector v, Vector target, Vector initialRocketLocation)
	{
		var anomaly = new Vector((target.X + initialRocketLocation.X) / 2, (target.Y + initialRocketLocation.Y) / 2);
		var d = (anomaly - v).Length;
		var normalizedVector = (anomaly - v).Normalize();
		return normalizedVector * (300 * d) / (d * d + 1);
	}

	private static Level GenerateLevel(string name, Gravity gravity, Vector target)
	{
		return new Level(name,
			new Rocket(initialRocketLocationConst, Vector.Zero, -0.5 * Math.PI),
			target,
			gravity,
			standardPhysics);
	}
}