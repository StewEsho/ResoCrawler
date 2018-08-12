using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{

	private List<GameObject> inventory;
	private int index;
	private bool nearChest = false;
	private Chest chest;
	
	// Use this for initialization
	void Start ()
	{
		index = 0;
		inventory = new List<GameObject>();
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
			inventory.Add(child.gameObject);
		}
		EnableInventoryItem(index);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire"))
		{
			if (nearChest)
			{
				AddToInventory(chest.Open(), inventory.Count < 1);
				nearChest = false;
			}
			else
			{
				StartCoroutine(inventory[index].GetComponent<IFireable>().Fire());
			}
		}
		else if (Input.GetButtonDown("Next"))
		{
			EnableInventoryItem((int) Mathf.Repeat(index + 1, inventory.Count));
		}
		else if (Input.GetButtonDown("Prev"))
		{
			EnableInventoryItem((int) Mathf.Repeat(index - 1, inventory.Count));
		}
	}

	private void EnableInventoryItem(int i)
	{
		try
		{
			inventory[this.index].SetActive(false);
			inventory[i].SetActive(true);
			index = i;
		}
		catch (IndexOutOfRangeException e)
		{
			Debug.LogException(e);
		}
	}

	public void AddToInventory(GameObject item, bool switchToItem = false)
	{
		GameObject obj = Instantiate(item, transform);
		obj.SetActive(false);
		inventory.Add(obj);
		if (switchToItem)
		{
			EnableInventoryItem(inventory.Count - 1);
		}
		Debug.Log(string.Format("Added {0} to player inventory", item));
	}

	public void AddChest(Chest chest)
	{
		this.chest = chest;
		nearChest = chest != null;
	}
}
