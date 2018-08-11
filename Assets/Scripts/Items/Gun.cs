using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
	private GameObject sprite;
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		sprite = transform.Find("Sprite").gameObject;
		anim = sprite.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.Play("Shoot");
			Shoot();
		}
	}

	protected abstract void Shoot();
}
