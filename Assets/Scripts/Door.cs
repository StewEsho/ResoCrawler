using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
	private ItemPopulator itemPopulator;
	
	// Use this for initialization
	void Start ()
	{
		itemPopulator = GameObject.Find("Tilemap").GetComponent<ItemPopulator>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			Debug.Log("DOOR");
			ItemManagement im = other.gameObject.GetComponentInChildren<ItemManagement>();
			if (im.keyCount >= 1)
			{
				im.keyCount--;
				itemPopulator.DoorCount--;
				itemPopulator.SpawnGoal();
				Destroy(gameObject);
			}
		}
	}
}
