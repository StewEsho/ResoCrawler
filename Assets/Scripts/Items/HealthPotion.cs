using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IConsumable
{

	private int potency = 15;
	
	// Use this for initialization
	void Start ()
	{
		potency += Random.Range(-5, 5);
	}

	public void Consume(GameObject player)
	{
		HealthManagement hm = player.GetComponentInChildren<HealthManagement>();
		hm.AdjustHealth(potency);
	}
}
