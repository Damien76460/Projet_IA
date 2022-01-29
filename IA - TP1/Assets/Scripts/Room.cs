using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<Room> neighbors;
    [SerializeField] Jewel jewelPrefab;
    [SerializeField] Dust dustPrefab;

    public void RegisterNeighbor(Room room)
    {
        neighbors.Add(room);
        room.neighbors.Add(this);
    }

    public void AddItem(int whichItem)
    {
        if (whichItem == 0)
        {
            Jewel jewel = Instantiate(jewelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
        else if (whichItem == 1)
        {
            Dust dust = Instantiate(dustPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
        else
        {
            Jewel jewel = Instantiate(jewelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
            Dust dust = Instantiate(dustPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
    }
}
