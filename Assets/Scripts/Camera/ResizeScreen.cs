using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResizeScreen : MonoBehaviour
{
	private Camera cam;
	private readonly int PPU = 16;
	private int UNITS = 50;
	private int RESOLUTION; 

	void Awake()
	{
		Screen.SetResolution(800, 800, false);//defaults to 800 px
	}
	
	void Start ()
	{
		RESOLUTION = UNITS * PPU;
		cam = GetComponent<Camera>();
		cam.orthographicSize = UNITS / 2.0f;
		Screen.SetResolution(RESOLUTION, RESOLUTION, false);
	}

	public void SetUnits(int units)
	{
		UNITS = units;
		cam.orthographicSize = UNITS / 2.0f;
		Screen.SetResolution(RESOLUTION, RESOLUTION, false);
		Debug.Log(string.Format("Resized the resolution to {0}", RESOLUTION));
	}
}
