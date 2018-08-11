using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorizer : MonoBehaviour
{
	private SpriteRenderer sprite;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		Color color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
		sprite.color = color;
	}
}
