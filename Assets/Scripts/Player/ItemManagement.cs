using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManagement : MonoBehaviour
{

	private List<GameObject> inventory;
	private int index;
	private bool nearChest = false;
	private Chest chest;
	[HideInInspector] public int keyCount = 3;
	[SerializeField] private Text keyCountText;
	
	// Use this for initialization
	void Start ()
	{
		keyCount = 3;
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
				AddToInventory(chest.Open(), inventory.Count < 1 || chest.SwitchToItem);
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

		keyCountText.text = keyCount.ToString();
	}

	private void EnableInventoryItem(int i)
	{
		try
		{
			inventory[this.index].SetActive(false);
			inventory[i].SetActive(true);
			index = i;
		}
		catch (ArgumentOutOfRangeException e)
		{
			Debug.LogException(e);
		}
	}

	public void AddToInventory(GameObject item, bool switchToItem = false)
	{
		if (item.GetComponentInChildren<Weapon>() != null)
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
		else if (item.GetComponentInChildren<IConsumable>() != null)
		{
			GameObject realItem = Instantiate(item);
			IConsumable consumable = realItem.GetComponentInChildren<IConsumable>();
			consumable.Consume(transform.root.gameObject);
		} else if (item.GetComponent<Key>() != null)
		{
			keyCount++;
		}
	}

	public void AddChest(Chest chest)
	{
		this.chest = chest;
		nearChest = chest != null;
	}

	public void ClearInventory()
	{
		foreach (var item in inventory)
		{
			Destroy(item);
		}
		inventory.Clear();
	}

	public List<Weapon> GetEquippedWeapons()
	{
		List<Weapon> weapons = new List<Weapon>();
		foreach (var item in inventory)
		{
			Weapon weaponScript = item.GetComponentInChildren<Weapon>();
			if (weaponScript != null)
			{
				weapons.Add(weaponScript);
			}
		}

		return weapons;
	}
}
