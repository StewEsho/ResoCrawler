using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCode : MonoBehaviour
{
	void Start () {
		
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Debug.Log("G key has been pressed.");
		}
		
	}
}
