using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Grid))]
public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private Tile[] tiles = new Tile[3];
    [SerializeField] private int mapSize;
    [SerializeField] private int numberOfRooms;

    // Use this for initialization
    void Start()
    {
        Tilemap background = transform.Find("Background").gameObject.GetComponent<Tilemap>(); //messy but w/e
        background.color = Random.ColorHSV(0, 1, 1, 1, 0.75f, 0.75f);
        Tilemap walls = transform.Find("Walls").gameObject.GetComponent<Tilemap>();
        GenerateMap(mapSize, numberOfRooms, background, walls);
    }

    public void GenerateMap(int size, int roomCount, Tilemap background, Tilemap walls)
    {
        List<Rect> rooms = GenerateDungeonRooms(size, roomCount);
        Debug.Log(rooms[0]);
        int[,] map = CreateTilemapArray(size, rooms);
        rooms = PutCenterRoomFirst(rooms, size);
        rooms.AddRange(ConnectRooms(rooms, map));
        map = CreateTilemapArray(size, rooms);

        //Render Map
        RenderMap(map, background, walls);
        //Put player in the dungeon
        Transform player = GameObject.FindWithTag("Player").transform;
        player.position = rooms[0].center;
    }

    public List<Rect> PutCenterRoomFirst(List<Rect> rooms, int size)
    {
        Vector2 center = new Vector2(size / 2.0f, size / 2.0f);
        Rect closestRoom = rooms[0];
        float closestDistance = Vector2.Distance(closestRoom.center, center);
        int index = 0;
        for (int i = 1; i < rooms.Count; i++)
        {
            if (Vector2.Distance(rooms[i].center, center) < closestDistance)
            {
                closestRoom = rooms[i];
                closestDistance = Vector2.Distance(rooms[i].center, center);
                index = i;
            }
        }

        rooms.Swap(index, 0);
        return rooms;
    }

    public Rect NewRoom(int x, int y, int w, int h, int gridSize)
    {
        //Create and return a Rect -centered- at (x, y)
        Rect room = new Rect(x - w / 2, y - h / 2, w, h);
        room.xMax = Math.Min(gridSize - 2, room.xMax);
        room.yMax = Math.Min(gridSize - 2, room.yMax);
        room.xMin = Math.Max(2, room.xMin);
        room.yMin = Math.Max(2, room.yMin);
        return room;
    }

    public List<Rect> GenerateDungeonRooms(int size, int roomCount)
    {
        Debug.Log("Generating dungeon rooms.");
        //Assuming the Dungeon grid is a size*size grid
        List<Rect> rooms = new List<Rect>();
        int failedAttempts = 0; // So the program does not get stuck placing rooms if no room can be found.
        while (rooms.Count < roomCount && failedAttempts < 30)
        {
            //Find random point
            int cx = Random.Range(5, size - 5);
            int cy = Random.Range(5, size - 5);
            //Random size
            int width = Random.Range(10, 25);
            int height = width + Random.Range(-2, 2);
            Rect room = NewRoom(cx, cy, width, height, size);

            //Check for intersections
            bool intersects = false;
            foreach (Rect r in rooms)
            {
                if (room.Overlaps(r))
                {
                    intersects = true;
                    failedAttempts++;
                    break;
                }
            }

            //If not intersecting, add to list of rooms
            if (!intersects)
            {
                rooms.Add(room);
                failedAttempts = 0;
            }
        }

        return rooms;
    }

    public int[,] CreateTilemapArray(int size, List<Rect> rooms)
    {
        Debug.Log("Creating Tilemap");
        int[,] map = new int[size, size];
        foreach (Rect room in rooms)
        {
            int x1 = (int) room.x;
            int y1 = (int) room.y;
            int x2 = x1 + ((int) room.width);
            int y2 = y1 + ((int) room.height);
            for (int u = x1; u < x2; u++)
            {
                for (int v = y1; v < y2; v++)
                {
                    try
                    {

                        map[u, v] = 1;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Debug.LogWarning(string.Format("OUT OF RANGE: ({0}, {1}) || {2}", u, v, room));
                    }
                }
            }
        }

        for (int x = 1; x < size-1; x++)
        {
            for (int y = 1; y < size-1; y++)
            {
                if (map[x, y] == 1)
                {
                    for (int r = -1; r <= 1; r++)
                    {
                        for (int c = -1; c <= 1; c++)
                        {
                            if (map[x + r, y + c] == 0)
                                map[x + r, y + c] = 2;
                        }
                    }
                }
            }
        }

        return map;
    }

    public void RenderMap(int[,] map, Tilemap background, Tilemap walls)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 2) // wall
                {
                    walls.SetTile(new Vector3Int(x, y, 0), tiles[2]);
                }
                else if (map[x, y] == 1)
                {
                    background.SetTile(new Vector3Int(x, y, 0), tiles[1]);
                }
            }
        }
    }

    public List<Rect> ConnectRooms(List<Rect> rooms, int[,] map)
    {
        Vector2 mapCenter = rooms[0].center;
        List<Rect> newRooms = new List<Rect>();
        foreach (Rect room in rooms.Skip(1))
        {
            //Get Directions
            Vector2 center = room.center;
            newRooms.Add(NewRoom((int) (center.x + mapCenter.x) / 2, (int) center.y + 1,
                (int) Mathf.Abs(center.x - mapCenter.x), 2, map.GetLength(0)));
            newRooms.Add(NewRoom((int) mapCenter.x + 1, (int) (center.y + mapCenter.y) / 2,
                2, (int) Mathf.Abs(center.y - mapCenter.y), map.GetLength(0)));
        }

        return newRooms;
    }
}