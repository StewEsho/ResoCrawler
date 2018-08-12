using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Sprite openedChestSprite; //todo: probably better to use an animator, right? w/e
    private bool isOpened;
    private bool switchToItem;
    private BoxCollider2D col;
    private Animator anim;
    private AudioSource audioSource;

    public void Start()
    {
        col = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public bool SwitchToItem
    {
        get { return switchToItem; }
    }

    public void SetItem(GameObject item)
    {
        this.item = item;
        transform.Find("Item Get").GetComponent<SpriteRenderer>().sprite =
            item.GetComponentInChildren<SpriteRenderer>().sprite;
        if (item.GetComponentInChildren<Weapon>() != null)
        {
            switchToItem = true;
        }
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
        col.enabled = false;
        audioSource.Play();
        anim.Play("ItemGet");
        return item;
    }
}