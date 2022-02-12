using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<Room> perception;
    public List<Room> beliefs;
    public List<Room> desires;

    public float speed = 1;
    public Room currentRoom;

    public int cycleCount = 0;
    public int jewelCollected = 0;
    public int dustCleaned = 0;

    private Environment environment;

    public void InitAgent()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        Debug.Log("Enter loop");
        while (true)
        {
            Debug.Log($"Cycle #{cycleCount}");

            Scan();
            UpdateMyStateBDI();

            List<Action> actionPlan = CreateActionPlan();

            yield return ExecuteActionPlan(actionPlan);

            cycleCount++;

            //Loop breaking condition
            if (cycleCount > 15)
            {
                Debug.Log("Exiting loop");
                yield break;
            }
        }
    }

    private void Scan()
    {
        if (environment == null)
        {
            environment = FindObjectOfType<Environment>();
        }
        perception = environment.RoomList;
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

    private List<Action> CreateActionPlan()
    {
        currentRoom = perception[0];
        List<Action> actionPlan = new List<Action>();
        actionPlan.Add(new Move(perception[1]));
        actionPlan.Add(new Move(perception[2]));
        actionPlan.Add(new Collect(perception[2]));
        return actionPlan;
    }

    private IEnumerator ExecuteActionPlan(List<Action> actionPlan)
    {
        foreach (var action in actionPlan)
        {
            yield return action.ExecuteAction(this);
        }
    }
}
