using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] private int maxHP = 15;
	private int HP;
	
	// Use this for initialization
	void Start ()
	{
		HP = maxHP;
	}

	private void Update()
	{
		if (HP <= 0)
		{
			//play death animation instead
			Destroy(gameObject);
		}
	}

	public void AdjustHealth(int amount)
	{
		HP = Mathf.Clamp(HP + amount, 0, maxHP);
	}
}
