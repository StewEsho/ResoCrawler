using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon
{
	[SerializeField] public GameObject Bullet;
	
	protected abstract void Shoot();

	public override IEnumerator Fire()
	{
		if (!CanFire)
			yield return null;
		else
		{
			CanFire = false;
			Anim.Play("Shoot", -1, 0);
			AudioSource.Play();
			Shoot();
			yield return new WaitForSeconds(FireRate);
			CanFire = true;
		}
		
	}
}
