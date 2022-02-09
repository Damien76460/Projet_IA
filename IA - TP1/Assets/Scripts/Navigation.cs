using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation 
{
    List<Room> roomListVisited = new List<Room>();
    Stack<List<Room>> PileAction = new Stack<List<Room>>();


    // DFS
    public void DFS(Room room)
    {
        Debug.Log("Room visitee : " + room.gameObject.name);
        roomListVisited.Add(room);

        /*
        if (room.jewel == true && room.dust == false)
        {
        }
        if (room.dust == true && room.jewel == false)
        {
        }
        if (room.dust == true && room.jewel == true)
        {
        }
        */

        for (int elem = 0; elem < room.neighbors.Count; elem++)
        {
            if (!roomListVisited.Contains(room.neighbors[elem]))
            {
                DFS(room.neighbors[elem]);
            }
        }

        Debug.LogWarning("Pas de chemin trouvé");

    } // FIN de la fonction DFS


    // BFS
    public void BFS(Room room)
    {

        Queue<Room> Queue = new Queue<Room>();
        Queue.Enqueue(room);
        Debug.Log("Room init visitée : " + room.gameObject.name);
        roomListVisited.Add(room);
        while (Queue.Count != 0)
        {
            Room current = Queue.Dequeue();
            for (int elem = 0; elem < current.neighbors.Count; elem++)
            {
                if (!roomListVisited.Contains(current.neighbors[elem]))
                {
                    Queue.Enqueue(current.neighbors[elem]);
                    Debug.Log("Room visitée : " + current.neighbors[elem].gameObject.name);
                    roomListVisited.Add(current.neighbors[elem]);
                }
            }

        }
        Debug.Log("NB de room visités : " + roomListVisited.Count);

    } // FIN de la fonction BFS 



} // FIN de la classe Navigation 
