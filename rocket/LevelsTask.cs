using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new Physics();
	static readonly Vector target = new Vector(600, 200);
	static readonly Vector initialRocketLocation = new Vector(200, 500);

	public static IEnumerable<Level> CreateLevels()
	{
		yield return GenerateLevel("Zero", (size, v) => Vector.Zero);
		yield return GenerateLevel("Heavy", (size, v) => new Vector(0, 0.9));
		yield return GenerateLevel("Up", (size, v) => new Vector(0, 300 / (v.Y + 300.0)));
		Gravity whiteHoleGravityDelegate = (size, v) => WhiteHoleGravity(v, target);
		yield return GenerateLevel("WhiteHole", whiteHoleGravityDelegate);
		Gravity blackHoleGravityDelegate = (size, v) => BlackHoleGravity(v, target, initialRocketLocation);
		yield return GenerateLevel("BlackHole", blackHoleGravityDelegate);
		Gravity blackAndWhiteDelegate = (size, v) => new Vector(whiteHoleGravityDelegate(size, v).X + blackHoleGravityDelegate(size, v).X, whiteHoleGravityDelegate(size, v).Y + blackHoleGravityDelegate(size, v).Y);
		yield return GenerateLevel("BlackAndWhite", (size, v) => new Vector(blackAndWhiteDelegate(size, v).X / 2, blackAndWhiteDelegate(size, v).Y / 2));
	}

	private static Vector WhiteHoleGravity(Vector v, Vector target)
	{
		var dX = v.X - target.X;
		var dY = v.Y - target.Y;

		Func<double, double> len = (x) => 140 * x / (x * x + 1);
		return new Vector(len(dX), len(dY));
	}

	/* FAILED TO PASS
	Как минимум один из тестов не пройден!
		Название теста: BlackHole
		Сообщение:
		Expected X: -34.61538461538462, Y: 46.15384615384615 but was X: -90, Y: 70.58823529411765
		Стек вызовов:
		   at func_rocket.checking.LevelsTests.AssertAreEqual(Vector expected, Vector actual) in /app/checking/LevelsTests.cs:line 97
		   at func_rocket.checking.LevelsTests.BlackHole() in /app/checking/LevelsTests.cs:line 69
*/
	private static Vector BlackHoleGravity(Vector v, Vector target, Vector initialRocketLocation)
	{
		var anomalyX = Math.Abs(Math.Abs(target.X) + Math.Abs(initialRocketLocation.X)) / 2;
		var anomalyY = Math.Abs(Math.Abs(target.Y) + Math.Abs(initialRocketLocation.Y)) / 2;
		Debug.WriteLine($"AnomalyX = {anomalyX}, anomalyY = {anomalyY}");
		Func<double, double> len = (x) => -300 * x / ((x * x) + 1);
		return new Vector(len(v.X - anomalyX), len(v.Y - anomalyY));
		//return new Vector(len(Math.Abs(v.X - anomalyX)), len(Math.Abs(v.Y - anomalyY)));
	}

	private static Level GenerateLevel(string name, Gravity gravity)
	{
		return new Level(name,
			new Rocket(initialRocketLocation, Vector.Zero, -0.5 * Math.PI),
			target,
			gravity,
			standardPhysics);
	}
}