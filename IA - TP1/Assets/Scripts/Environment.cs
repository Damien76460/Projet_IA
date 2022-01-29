using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] Vector2Int size = new Vector2Int(5, 5);
    [SerializeField] Room roomPrefab;
    List<Room> roomList;
    Room[,] roomArray;


    public float spawnRate;

    public static Dictionary<Direction, Vector2> Direction2DValues = new Dictionary<Direction, Vector2>
    {
        {Direction.Up, Vector2.up},
        {Direction.Down, Vector2.down},
        {Direction.Left, Vector2.left},
        {Direction.Right, Vector2.right}
    };

    public static Dictionary<Direction, Vector3> Direction3DValues = new Dictionary<Direction, Vector3>
    {
        {Direction.Up, Vector3.forward},
        {Direction.Down, Vector3.back},
        {Direction.Left, Vector3.left},
        {Direction.Right, Vector3.right}
    };

    // Start is called before the first frame update
    void Start()
    {
        BuildEnvironment();
        SpawnItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Build Environment")]
    void BuildEnvironment()
    {
        ClearEnvironment();
        
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Room room = Instantiate(roomPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                room.name = $"Room ({x},{y})";
                roomArray[x, y] = room;
                roomList.Add(room);
                //Get horizontal neighbor
                if (x > 0)
                {
                    roomArray[x, y].RegisterNeighbor(roomArray[x - 1, y]);
                }
                //Get vertical neighbor
                if (y > 0)
                {
                    roomArray[x, y].RegisterNeighbor(roomArray[x, y - 1]);
                }
            }
        }
    }

    [ContextMenu("Clear Environment")]
    void ClearEnvironment()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        roomList = new List<Room>();
        roomArray = new Room[size.x, size.y];
    }

    void SpawnItems()
    {
        foreach (Room room in roomList)
        {
            if (GetSpawnBool())
            {
                int getItem = GetItemToSpawn();
                room.AddItem(getItem);
            }
        }
    }

    bool GetSpawnBool()
    {
        float randomProbability = Random.value;
        if (randomProbability > spawnRate)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }

    int GetItemToSpawn()
    {
        float randomItem = Random.value;
        int whatToSpawn;
        switch (randomItem)
        {
            //will spawn a jewel (40% rate)
            case <= 0.4f:
                whatToSpawn = 0;
                return whatToSpawn;
            //will spawn dust (40% rate)
            case <= 0.8f:
                whatToSpawn = 1;
                return whatToSpawn;
            //will spawn both (20% rate)
            default:
                whatToSpawn = 2;
                return whatToSpawn;
        }
    }
}
