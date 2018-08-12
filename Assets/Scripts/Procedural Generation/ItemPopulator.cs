using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Manages chests and enemy placement in rooms when procedurally generating dungeons.
 */
public class ItemPopulator : MonoBehaviour
{
    [SerializeField] GameObject chest;
    [SerializeField] List<GameObject> chestWeapons; //One of each of these is put into a chest
    [SerializeField] List<GameObject> chestItems; //The rest of the chests are filled out with items from this list
    public int ChestCount = 15;

    private int totalChests = 0;

    //todo: add doors + keys
    private List<GameObject> chestObjectsToInstantiate = new List<GameObject>(); //todo: not needed? maybe for keys? idk yet
    
    public void PopulateRoomsWithChests(List<Rect> rooms)
    {
        Debug.Log("Populating rooms with chests!");
        //copy lists since they will be mutated.
        List<Rect> localRooms = new List<Rect>(rooms);
        List<GameObject> localChestWeapons = new List<GameObject>(chestWeapons);
        localRooms.RemoveAt(0); //Remove starting room.
        localRooms.Shuffle();
        Debug.Log(localRooms);
        //Add the correct number of chests
        while (totalChests < ChestCount)
        {
            foreach (Rect room in localRooms)
            {
                if (totalChests >= ChestCount)
                {
                    return; //no more chests needed!
                }
                
                Vector2 pos = new Rect(room.position + (Vector2.one), room.size - 2 * Vector2.one).RandomPoint();
                if (localChestWeapons.Count > 0)
                {
                    GameObject newChest = Instantiate(chest, pos, Quaternion.identity, transform);
                    int index = Random.Range(0, localChestWeapons.Count);
                    newChest.GetComponent<Chest>().SetItem(localChestWeapons[index]);
                    localChestWeapons.RemoveAt(index);
                }
                else if (chestItems.Count > 0)
                {
                    GameObject newChest = Instantiate(chest, pos, Quaternion.identity, transform);
                    int index = Random.Range(0, chestItems.Count);
                    newChest.GetComponent<Chest>().SetItem(chestItems[index]);    
                }
                else
                {
                    return; // no more items can be added, so no more chests are created.
                }
            }
                
                totalChests++;
        }
        localRooms.Shuffle();
    }
}    