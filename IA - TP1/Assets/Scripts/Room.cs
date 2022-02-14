using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> neighbors = new List<Room>();
    public Dictionary<Room, Direction> neighborDirection = new Dictionary<Room, Direction>();
    public Dictionary<Direction, Room> directedNeighbor = new Dictionary<Direction, Room>();
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

        directedNeighbor.Add(direction, room);
        neighborDirection.Add(room, direction);

        Direction oppositeDirection = DirectionUtils.GetOppositeDirection(direction);

        room.neighborDirection.Add(this, oppositeDirection);
        room.directedNeighbor.Add(oppositeDirection, this);
    }
}
