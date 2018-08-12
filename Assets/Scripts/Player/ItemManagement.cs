using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{

	private List<GameObject> inventory;
	private int index;
	
	// Use this for initialization
	void Start ()
	{
		index = 0;
		inventory = new List<GameObject>();
		foreach (Transform child in transform)
		{
			inventory.Add(child.gameObject);
			EnableInventoryItem(index);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Next"))
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
}
