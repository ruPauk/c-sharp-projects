using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
		var potentialMaxList = new LinkedList<double>();
		var currentWindow = new LinkedList<DataPoint>();
		foreach (var point in data)
		{
			currentWindow.AddLast(point);
			ProcessPotentialMax(point.OriginalY, potentialMaxList);
			if (currentWindow.Count > windowWidth)
			{
				var first = currentWindow.First.Value;
				currentWindow.RemoveFirst();
				potentialMaxList.Remove(first.OriginalY);
			}
			var result = point.WithMaxY(potentialMaxList.First.Value);
			yield return result;
		}
	}

	public static void ProcessPotentialMax(double value, LinkedList<double> potentialMaxList)
	{
		while (potentialMaxList.Count > 0 && potentialMaxList.Last.Value < value)
			potentialMaxList.RemoveLast();
		potentialMaxList.AddLast(value);
	}
}