using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{
	private int maxHealth = 50;
	private int health;
	private ResizeScreen rs;

	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		rs = Camera.main.GetComponent<ResizeScreen>();
		if (rs == null)
		{
			Debug.LogError("Camera does not have ResizeScreen Component");
		}
		else
		{
			rs.SetUnits(health);
		}
	}

	public void AdjustHealth(int amount)
	{
		health -= amount;
		rs.SetUnits(health);
	}

	public bool IsDead()
	{
		return health <= 0;
	}

	public int GetHealth()
	{
		return health;
	}
}
