using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCode : MonoBehaviour
{
	private int PPU = 16;
	private int SCREENSIZE = 800;
	private int UNITS;
	private Camera cam;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(SCREENSIZE, SCREENSIZE, false);
		UNITS = (int) SCREENSIZE / PPU;
		cam = Camera.main; //todo: make an actual script.
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Debug.Log("Wow");
			UNITS--;
			SCREENSIZE = UNITS * PPU;
			cam.orthographicSize = UNITS / 2.0f;
			Screen.SetResolution(SCREENSIZE, SCREENSIZE, false);
		}
		
	}
}
