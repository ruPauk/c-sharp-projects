using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		var windowedData = new Queue<DataPoint>();
		double sum = 0;
		foreach (var dataPoint in data)
		{
			windowedData.Enqueue(dataPoint);
			sum += dataPoint.OriginalY;
			if (windowedData.Count > windowWidth)
				sum -= windowedData.Dequeue().OriginalY;
			yield return dataPoint.WithAvgSmoothedY(sum / windowedData.Count);
		}
	}
}