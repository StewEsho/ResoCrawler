using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Grid))]
public class ProceduralGeneration : MonoBehaviour {

	[SerializeField] private Tile[] tiles = new Tile[3];
	
	// Use this for initialization
	void Start () {
		Tilemap background = transform.Find("Background").gameObject.GetComponent<Tilemap>(); //messy but w/e
		Tilemap walls = transform.Find("Walls").gameObject.GetComponent<Tilemap>();
		GenerateMap(50, 5, background, walls);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Rect NewRoom(int x, int y, int w, int h, int gridSize)
	{
		//Make sure the room do not exceed the size of the grid.
		if (x + (w / 2) > gridSize)
		{
			w = (gridSize - x - 1) * 2;
		}
		if (x - (w / 2) < 0)
		{
			w = (x - 1) * 2;
		}
		if (y + (h / 2) > gridSize)
		{
			h = (gridSize - y - 1) * 2;
		}
		if (y - (h / 2) < 0)
		{
			h = (y - 1) * 2;
		}
		
		//Create and return a Rect -centered- at (x, y)
		return new Rect(x - w/2, y - h/2, w, h);
	}

	public List<Rect> GenerateDungeonRooms(int size, int roomCount)
	{
		Debug.Log("Generating dungeon rooms.");
		//Assuming the Dungeon grid is a size*size grid
		List<Rect> rooms = new List<Rect>();
		int failedAttempts = 0;
		while (rooms.Count < roomCount)
		{
			//Find random point
			int cx = Random.Range(5, size-5);
			int cy = Random.Range(5, size-5);
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
					break;
				}
			}
			
			//If not intersecting, add to list of rooms
			if (!intersects)
			{
				rooms.Add(room);
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
					if (u == (int) room.xMin || v == (int) room.yMin || u == (int) room.xMax - 1 || v == (int) room.yMax - 1)
					{
						map[u, v] = 2;
						Debug.Log(String.Format("({0}, {1}) --> 2", u, v));
					}
					else
					{
						map[u, v] = 1;
						Debug.Log(String.Format("({0}, {1}) --> 1", u, v));
					}
					
					
					Debug.Log(map[u, v]);
				}
			}

			Debug.Log(room);
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

	public void GenerateMap(int size, int roomCount, Tilemap background, Tilemap walls)
	{
		List<Rect> rooms = GenerateDungeonRooms(size, roomCount);
		RenderMap(CreateTilemapArray(size, rooms), background, walls);
		Transform player = GameObject.FindWithTag("Player").transform;
		player.position = rooms[0].center;
	}
}
