using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Gun : MonoBehaviour
{
	private GameObject sprite;
	private Animator anim;
	public float FireRate = 0.1f;
	private bool canFire = true;
	private AudioSource audioSource;

	// Use this for initialization
	protected void Start ()
	{
		sprite = transform.Find("Sprite").gameObject;
		anim = sprite.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire && Input.GetButtonDown("Fire"))
		{
			StartCoroutine(Fire());
		}
	}

	protected abstract void Shoot();

	IEnumerator Fire()
	{
		canFire = false;
		anim.Play("Shoot", -1, 0);
		audioSource.Play();
		Shoot();
		yield return new WaitForSeconds(FireRate);
		canFire = true;
	}
}
