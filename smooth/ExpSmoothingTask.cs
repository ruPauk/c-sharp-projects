using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
		DataPoint prevPoint = null;
		foreach (var dataPoint in data)
		{
			if (prevPoint == null)
			{
				prevPoint = dataPoint.WithExpSmoothedY(dataPoint.OriginalY); ;
				yield return prevPoint;
			}
			else
			{
				var tmp = prevPoint.ExpSmoothedY + alpha * (dataPoint.OriginalY - prevPoint.ExpSmoothedY);
				prevPoint = dataPoint.WithExpSmoothedY(tmp);
				yield return prevPoint;
			}
		}
	}
}