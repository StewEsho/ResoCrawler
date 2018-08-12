using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Broadsword : MonoBehaviour
{

	private Collider2D col;
	private bool canAttack = true;
	private Animator anim;
	public float SwingRate = 0.5f;
	public int Damage = 6;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		col = GetComponent<Collider2D>();
		anim = GetComponentInChildren<Animator>();
		Debug.Log(anim);
	}
	
	// Update is called once per frame
	void Update () {
		if (canAttack && Input.GetButtonDown("Fire"))
		{
			canAttack = false;
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack()
	{
		col.enabled = true;
		anim.Play("Swing", -1, 0);
		audioSource.Play();
		yield return new WaitForSeconds(SwingRate);
		col.enabled = false;
		canAttack = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().AdjustHealth(-Damage);
		}
	}
}
