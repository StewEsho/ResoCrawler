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

	public static bool BufferedOverlap(this Rect rect, Rect other, int buffer)
	{
		Rect bufferedRect = new Rect(rect.position - (buffer * Vector2.one), rect.size + (2 * buffer * Vector2.one));
		return bufferedRect.Overlaps(other);
	}

}
