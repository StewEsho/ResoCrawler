using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
	public int Damage = 3;
	
	void Start()
	{
		Destroy(gameObject, 1f);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!other.transform.CompareTag("Player") && !other.transform.CompareTag("Bullet"))
			Destroy(gameObject);
		if (other.transform.CompareTag("Enemy"))
		{
			other.gameObject.GetComponent<Enemy>().AdjustHealth(-Damage);
		}
	}
}
