using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<Room> perception;
    public List<Room> objectsLocation;
    public List<Room> sortedGoals;
    bool testOnce = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(testOnce)
        {
            SortByPriority();
            testOnce = false;
        }
  
    }

    void Scan()
    {
        perception = FindObjectOfType<Environment>().RoomList;
    }

    void FindObjects()
    {
        foreach (Room room in perception)
        {
            if (room.jewel != null || room.dust != null)
            {
                objectsLocation.Add(room);
            }
        }
    }

    void SortByPriority()
    {
        Scan();
        FindObjects();
        AddSpecificObject(objectsLocation, sortedGoals, "jewel");
        AddSpecificObject(objectsLocation, sortedGoals, "dust");
        AddSpecificObject(objectsLocation, sortedGoals, "both");
    }

    void AddSpecificObject(List<Room> checkedList, List<Room> newList, string whichObject)
    {
        foreach (Room room in checkedList)
        {
            switch (whichObject)
            {
                case "jewel":
                    if (room.jewel != null && room.dust == null)
                    {
                        newList.Add(room);
                    }
                    break;
                case "dust":
                    if (room.jewel == null && room.dust != null)
                    {
                        newList.Add(room);
                    }
                    break;
                case "both":
                    if (room.jewel != null && room.dust != null)
                    {
                        newList.Add(room);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
