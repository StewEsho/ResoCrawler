using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion : MonoBehaviour, IConsumable
{

	private float potency = 2.0f;
	private float duration = 10f;
	
	// Use this for initialization
	void Start ()
	{
		potency += Random.Range(-0.75f, 0.5f);
		GetComponentInChildren<SpriteRenderer>().color = Color.clear;
		Destroy(gameObject, 15);
	}

	public void Consume(GameObject player)
	{
		ItemManagement itemManager = player.GetComponentInChildren<ItemManagement>();
		List<Weapon> weapons = itemManager.GetEquippedWeapons();
		SpriteRenderer sprite = player.transform.Find("Sprite").GetComponent<SpriteRenderer>();
		StartCoroutine(AmplifyDamage(weapons, sprite));
	}

	private IEnumerator AmplifyDamage(List<Weapon> weapons, SpriteRenderer sprite)
	{
		Debug.Log(sprite);
		foreach (Weapon weapon in weapons)
		{
			weapon.DamageMultiplier = potency;
		}
		sprite.color = Color.blue;

		yield return new WaitForSeconds(duration);
		
		foreach (Weapon weapon in weapons)
		{
			weapon.DamageMultiplier = 1.0f;
		}
		sprite.color = Color.white;
	}
}
