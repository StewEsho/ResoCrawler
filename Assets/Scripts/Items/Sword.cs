using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sword : Weapon {
	private void OnDisable()
	{
		Col.enabled = false;
		CanFire = true;
	}
	
	public override IEnumerator Fire()
	{
		if (!CanFire)
		{
			yield return null;
		}
		else
		{
			Col.enabled = true;
			CanFire = false;
			Anim.Play("Swing", -1, 0);
			AudioSource.Play();
			yield return new WaitForSeconds(FireRate);
			CanFire = true;
			yield return new WaitForSeconds(0.05f);
			Col.enabled = false;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<Enemy>().AdjustHealth(- (int) (Damage * DamageMultiplier));
		}
	}
}
