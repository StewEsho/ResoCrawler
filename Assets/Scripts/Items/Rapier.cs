using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Rapier : MonoBehaviour, IFireable {

	private bool canAttack = true;
	private Animator anim;
	private Collider2D col;
	public float SwingRate = 0.5f;
	public int Damage = 6;
	private AudioSource audioSource;

	private void OnDisable()
	{
		col.enabled = false;
		canAttack = true;
	}

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		col = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		Debug.Log(anim);
	}

	public IEnumerator Fire()
	{
		if (!canAttack)
		{
			yield return null;
		}
		else
		{
			col.enabled = true;
			canAttack = false;
			anim.Play("Stab", -1, 0);
			audioSource.Play();
			yield return new WaitForSeconds(SwingRate);
			canAttack = true;
			yield return new WaitForSeconds(0.05f);
			col.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().AdjustHealth(-Damage);
		}
	}
}
