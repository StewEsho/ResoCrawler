using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

	public static void Swap<T>(this List<T> list, int index1, int index2)
	{
		if (index1 != index2)
		{
			T temp = list[index1];
			list[index1] = list[index2];
			list[index2] = temp;
		}
	}

	public static void Shuffle<T>(this List<T> list)
	{
		//Fisher-Yates Shuffle
		int n = list.Count;
		for (int i = n - 1; i > 1; i--)
		{
			int j = Random.Range(0, i + 1);
			list.Swap(i, j);
		}
	}

	public static bool BufferedOverlap(this Rect rect, Rect other, int buffer)
	{
		Rect bufferedRect = new Rect(rect.position - (buffer * Vector2.one), rect.size + (2 * buffer * Vector2.one));
		return bufferedRect.Overlaps(other);
	}

	public static Vector2 RandomPoint(this Rect rect)
	{
		float x = Random.Range(rect.xMin, rect.xMax);
		float y = Random.Range(rect.yMin, rect.yMax);
		return new Vector2(x, y);
	}

}
