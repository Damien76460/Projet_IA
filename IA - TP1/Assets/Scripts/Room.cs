using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> neighbors;
    public GameObject jewel;
    public GameObject dust;

    public void RegisterNeighbor(Room room)
    {
        neighbors.Add(room);
        room.neighbors.Add(this);
    }
}
