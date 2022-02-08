using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> neighbors;
    public GameObject jewel = null;
    public GameObject dust = null;

    public void RegisterNeighbor(Room room)
    {
        neighbors.Add(room);
        room.neighbors.Add(this);
    }
}
