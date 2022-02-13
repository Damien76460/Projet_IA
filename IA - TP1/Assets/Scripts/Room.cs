using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> neighbors = new List<Room>();
    public Dictionary<Direction, Room> directedNeighbors = new Dictionary<Direction, Room>();
    public GameObject jewel = null;
    public GameObject dust = null;

    public void RegisterNeighbor(Room room)
    {
        neighbors.Add(room);
        room.neighbors.Add(this);
    }

    public void RegisterNeighbor(Room room, Direction direction)
    {
        RegisterNeighbor(room);
        directedNeighbors.Add(direction, room);
        room.directedNeighbors.Add(DirectionUtils.GetOppositeDirection(direction), this);
    }
}
