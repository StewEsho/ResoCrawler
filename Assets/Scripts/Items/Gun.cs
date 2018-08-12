using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Gun : MonoBehaviour, IFireable
{
	private GameObject sprite;
	private Animator anim;
	public float FireRate = 0.1f;
	private bool canFire = true;
	private AudioSource audioSource;

	void OnDisable()
	{
		canFire = true;
	}

	// Use this for initialization
	protected void Start ()
	{
		sprite = transform.Find("Sprite").gameObject;
		anim = sprite.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	protected abstract void Shoot();

	public IEnumerator Fire()
	{
		if (!canFire)
			yield return null;
		else
		{
			canFire = false;
			anim.Play("Shoot", -1, 0);
			audioSource.Play();
			Shoot();
			yield return new WaitForSeconds(FireRate);
			canFire = true;
		}
		
	}
}
