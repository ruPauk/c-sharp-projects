using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var bestOrder = MakeTrivialPermutation(checkpoints.Length);
			int[] workOrder = new int[checkpoints.Length];
			bestOrder.CopyTo(workOrder, 0);
			double bestResult = checkpoints.GetPathLength(bestOrder);
			MixPermutation(workOrder, 0, 0.0, ref bestOrder, bestResult, checkpoints);
			return bestOrder;
		}

		private static double AccumulatePathLength(double currentPathLength, Point[] checkpoints,
			int indexFirst, int indexSecond)
		{
			currentPathLength += checkpoints[indexFirst].DistanceTo(checkpoints[indexSecond]);
			return currentPathLength;
		}

		//NEED TO FIX "ref int[] bestOrder" and length of this method
		private static double MixPermutation(int[] curArray, int curElement, double currentPathLength,
			ref int[] bestOrder, double bestPathLength, Point[] checkpoints)
		{
			if (curElement == (curArray.Length - 1))
			{
				if (currentPathLength < bestPathLength)
				{
					bestOrder = curArray;
					return currentPathLength;
				}
				else
				{
					return bestPathLength;
				}
			}
			for (int i = curElement + 1; i < curArray.Length; i++)
			{
				var tmpLength = AccumulatePathLength(currentPathLength, checkpoints, curArray[curElement], curArray[i]);
				if (tmpLength > bestPathLength)
					return bestPathLength;
				else
				{
					var tmpArray = GetNewIterationArray(curArray, curElement + 1, i);
					bestPathLength = MixPermutation(tmpArray, curElement + 1, tmpLength, ref bestOrder, bestPathLength, checkpoints);
				}
			}
			return bestPathLength;
		}

		private static int[] GetNewIterationArray(int[] curArray, int curElement, int chosenElem)
		{
			var newCurArray = new int[curArray.Length];
			curArray.CopyTo(newCurArray, 0);
			if (curElement == chosenElem)
			{
				newCurArray[curElement] = curArray[chosenElem];
			}
			else
			{
				ExchangeValuesInArray(newCurArray, curElement, chosenElem);
			}
			return newCurArray;
		}

		private static void PrintListOfArrays(List<int[]> arr)
		{
			foreach (var elem in arr)
			{
				foreach (var i in elem)
				{
					Console.Write(i + " ");
				}
				Console.WriteLine();
			}
		}

		private static void PrintArray(int[] arr, string message, double bestLength, double currentLength)
		{
			Console.Write(message + ": ");
			foreach (var elem in arr)
			{
				Console.Write(elem + " ");
			}
			Console.Write(" -> best = " + bestLength + " current = " + currentLength);
			Console.WriteLine();
		}

		private static int[] MakeTrivialPermutation(int size)
		{
			var bestOrder = new int[size];
			for (int i = 0; i < bestOrder.Length; i++)
				bestOrder[i] = i;
			return bestOrder;
		}

		private static void ExchangeValuesInArray(int[] arr, int indexA, int indexB)
		{
			int tmp = arr[indexA];
			arr[indexA] = arr[indexB];
			arr[indexB] = tmp;
			return;
		}

		private static bool CompareDoubles(double bestPathLength, double currentPathLength)
		{
			var tmp = currentPathLength - bestPathLength;
			return tmp > 1e-9;
		}
	}
}