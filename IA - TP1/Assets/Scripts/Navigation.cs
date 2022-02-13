using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Navigation
{

    public List<Room> BFS(Room StartRoom, Room EndRoom, Environment env)
    {
        // List of Rooms who is visited 
        List<Room> roomListVisited = new List<Room>();
        // Creation of the Queue
        Queue<Room> Queue = new Queue<Room>();
        Queue.Enqueue(StartRoom);
        roomListVisited.Add(StartRoom);
        //Init Reconstruction_Path at null matrice 5x5

        Dictionary<Room, Room> Reconstruction_Path = new Dictionary<Room, Room>(env.roomList.Count);
        for (int i = 0; i < env.roomList.Count; i++)
        {
            Reconstruction_Path.Add(env.roomList[i], null);
        }

        while (Queue.Count != 0)
        {
            Room current = Queue.Dequeue();
            for (int elem = 0; elem < current.neighbors.Count; elem++)
            {
                if (!roomListVisited.Contains(current.neighbors[elem]))
                {
                    Queue.Enqueue(current.neighbors[elem]);
                    roomListVisited.Add(current.neighbors[elem]);
                    Reconstruction_Path[current.neighbors[elem]] = current;
                }
            }
        }
        return build_path(EndRoom, Reconstruction_Path);
    }



    public List<Room> A_Star(Room StartRoom, Room EndRoom, Environment env)
    {
        // Dictionary (Cout,distance,heuristique)
        Dictionary<Room, Vector3> Heuristique = new Dictionary<Room, Vector3>(env.roomList.Count);
        for (int i = 0; i < env.roomList.Count; i++)
        {
            Heuristique.Add(env.roomList[i], new Vector3(0,0,0)); 
        }

        // Creation of openList and closeList
        List<Room> openList = new List<Room>();
        List<Room> closeList = new List<Room>();


        openList.Add(StartRoom);
        
        //Init Reconstruction_Path at null matrice 5x5

        Dictionary<Room, Room> Reconstruction_Path = new Dictionary<Room, Room>(env.roomList.Count);
        for (int i = 0; i < env.roomList.Count; i++)
        {
            Reconstruction_Path.Add(env.roomList[i], null);
        }

        float Room_MinHeuristique = Heuristique[StartRoom].z;
        // While the Queue isn't empty 
        while (openList.Count != 0)
        {
            // Find the node with the least heuristic
            Room current = openList[0];
            openList.Remove(current);

            for (int elem = 0; elem < openList.Count; elem++)
            {
                if (Heuristique[openList[elem]].z < Room_MinHeuristique)
                {
                    Room_MinHeuristique = Heuristique[openList[elem]].z;
                    current = openList[elem];
                }
            }
           
            // Neighbors of our current node 
            for (int elem = 0; elem < current.neighbors.Count; elem++)
            {
                
                if(current == EndRoom)
                {
                    break;
                }

                if(!(closeList.Contains(current.neighbors[elem])))
                {
                    // cout, distance, heuristique 
                    Vector3 var = new Vector3(0, 0, 0);
                    var.x = Heuristique[current].x + 1;
                    var.y = Mathf.Abs(current.neighbors[elem].transform.position.x - EndRoom.transform.position.x)
                        + Mathf.Abs(current.neighbors[elem].transform.position.z - EndRoom.transform.position.z);
                    var.z = var.x + var.y;
                    Heuristique[current.neighbors[elem]] = var;

                    if (openList.Contains(current.neighbors[elem]) && Heuristique[current.neighbors[elem]].z < var.z)
                    {
                        continue;
                    }

                    else
                    {
                        openList.Add(current.neighbors[elem]);
                        Reconstruction_Path[current.neighbors[elem]] = current;
                    }
                    
                }

            } // End of for loop
            closeList.Add(current);


        } // End of while
        return build_path(EndRoom, Reconstruction_Path);
    } // End of fonction


    public List<Room> build_path(Room EndRoom, Dictionary<Room, Room> Reconstruction_Path)
    {
        // Init Return_Path
        List<Room> Return_Path = new List<Room>();

        Return_Path.Add(EndRoom);
        while (EndRoom != null)
        {
            if (Reconstruction_Path[EndRoom] == null)
            {
                break;
            }
            else
            {
                EndRoom = Reconstruction_Path[EndRoom];
                Return_Path.Add(EndRoom);
            }
        }

        Return_Path.Reverse();
        Debug.Log("Build Path : ");
        for (int elem = 0; elem < Return_Path.Count; elem++)
        {
            Debug.Log("Result" + Return_Path[elem]);
        }
        return Return_Path;
    }



}
