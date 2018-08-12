using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
	private GameObject sprite;
	private Animator anim;
	protected float FireRate = 0.1f;
	private bool canFire = true;

	// Use this for initialization
	protected void Start ()
	{
		sprite = transform.Find("Sprite").gameObject;
		anim = sprite.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire && Input.GetButtonDown("Fire"))
		{
			anim.Play("Shoot");
			StartCoroutine(RestBetweenFire());
			Shoot();
		}
	}

	protected abstract void Shoot();

	IEnumerator RestBetweenFire()
	{
		canFire = false;
		yield return new WaitForSeconds(FireRate);
		canFire = true;
	}
}
