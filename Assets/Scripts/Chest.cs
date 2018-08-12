using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Sprite openedChestSprite; //todo: probably better to use an animator, right? w/e
    private bool isOpened = false;
    private bool switchToItem = false;

    public bool SwitchToItem
    {
        get { return switchToItem; }
    }

    public void SetItem(GameObject item)
    {
        this.item = item;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            ItemManagement inventory = other.GetComponentInChildren<ItemManagement>();
            inventory.AddChest(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            ItemManagement inventory = other.GetComponentInChildren<ItemManagement>();
            inventory.AddChest(null);
        }
    }

    public GameObject Open()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = openedChestSprite;
        isOpened = true;
        return item;
    }
}