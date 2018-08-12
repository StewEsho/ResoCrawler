using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] private int maxHP = 15;
	private int HP;
	private Animator anim;
	private Rigidbody2D rb;
	private AudioSource audioSource;
	
	// Use this for initialization
	void Start ()
	{
		HP = maxHP;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}

	public IEnumerator Die()
	{
		audioSource.Play();
		yield return new WaitForSeconds(0.35f);
		Destroy(gameObject);
	}

	public void AdjustHealth(int amount)
	{
		HP = Mathf.Clamp(HP + amount, 0, maxHP);
		if (amount < 0)
		{
			anim.Play("Damage");
		}
		if (HP <= 0)
		{
			//play death animation instead? (NAH)
			StartCoroutine(Die());
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.CompareTag("Bullet"))
		{
			Vector2 dir = other.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
			rb.AddForce(150 * -dir.normalized, ForceMode2D.Impulse);
		}
	}
}
