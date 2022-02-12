using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<Room> perception;
    public List<Room> beliefs;
    public List<Room> desires;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //A placer dans les coroutines
        Scan();
        UpdateMyStateBDI();
    }

    void Scan()
    {
        perception = FindObjectOfType<Environment>().RoomList;
    }

    void AddBelief()
    {
        beliefs.Clear();
        foreach (Room room in perception)
        {
            if (room.jewel != null || room.dust != null)
            {
                beliefs.Add(room);
            }
        }
    }

    void AddDesire()
    {
        desires.Clear();
        AddSpecificObject(beliefs, desires, "jewel");
        AddSpecificObject(beliefs, desires, "dust");
        AddSpecificObject(beliefs, desires, "both");
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

    void UpdateMyStateBDI()
    {
        AddBelief();
        AddDesire();
    }
}
