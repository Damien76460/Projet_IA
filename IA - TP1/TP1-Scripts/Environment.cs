using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] Vector2Int size = new Vector2Int(5, 5);
    [SerializeField] Room roomPrefab;
    [SerializeField] GameObject jewelPrefab;
    [SerializeField] GameObject dustPrefab;
    List<Room> roomList;
    public List<Room> RoomList { get => roomList; }
    Room[,] roomArray;

    //Environment variables about items spawning
    public int itemsAmountOnStart;
    public float spawnRate;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float jewelRate;
    public float dustRate;

    public void InitEnvironment()
    {
        BuildEnvironment();
        SpawnInitItems();
        StartCoroutine(Loop());
    }

    //Function that will be executed as a Thread for the Environment
    IEnumerator Loop()
    {
        while (true)
        {
            float spawnTime = Random.Range(minSpawnTime,maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
            SpawnItems();
        }
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
                    roomArray[x, y].RegisterNeighbor(roomArray[x - 1, y], Direction.Left);
                }
                //Get vertical neighbor
                if (y > 0)
                {
                    roomArray[x, y].RegisterNeighbor(roomArray[x, y - 1], Direction.Down);
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

    //Spawn random items in random selected rooms
    void SpawnItems()
    {   
        int randomRoom = Random.Range(0, roomList.Count);
        Room room = roomList[randomRoom];
        if (room.dust == null && room.jewel == null)
        {
            if (Random.value < spawnRate)
            {
                float randomItem = Random.value;
                GameObject jewel = null, dust = null;
                switch (randomItem)
                {
                    //Will spawn a jewel
                    case float n when (n <= jewelRate):
                        jewel = Instantiate(jewelPrefab, room.transform.position, Quaternion.identity, room.transform);
                        break;
                    //Will spawn dust
                    case float n when (n > jewelRate && n <= jewelRate+dustRate):
                        dust = Instantiate(dustPrefab, room.transform.position, Quaternion.identity, room.transform);
                        break;
                    //Will spawn both items
                    default:
                        jewel = Instantiate(jewelPrefab, room.transform.position, Quaternion.identity, room.transform);
                        dust = Instantiate(dustPrefab, room.transform.position, Quaternion.identity, room.transform);
                        break;
                }
                room.jewel = jewel;
                room.dust = dust;
            }
        }
    }

    //Spawn the desired amount of items before start
    void SpawnInitItems()
    {
        float tmpSpawn = spawnRate;
        spawnRate = 1f;
        for (int i = 0; i <= itemsAmountOnStart; i++)
        {
            SpawnItems();
        }
        spawnRate = tmpSpawn;
    }
}
