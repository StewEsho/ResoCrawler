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
	
}
