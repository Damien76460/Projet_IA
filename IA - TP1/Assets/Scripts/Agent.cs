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

    private int cycleCount = 0;
    private int jewelCollected = 0;
    private int dustCleaned = 0;

    private GameManager gameManager;

    public int CycleCount
    {
        get => cycleCount; 
        set
        {
            cycleCount = value;
            gameManager.UIManager.iterationText.text = $"ITERATION : {cycleCount}";
        }
    }
    public int JewelCollected 
    { 
        get => jewelCollected;
        set
        {
            jewelCollected = value;
            gameManager.UIManager.jewelText.text = jewelCollected.ToString();
        }
    }
    public int DustCleaned 
    { 
        get => dustCleaned;
        set
        {
            dustCleaned = value;
            gameManager.UIManager.dustText.text = dustCleaned.ToString();
        }
    }

    public void InitAgent(GameManager gameManager, Room startRoom)
    {
        this.gameManager = gameManager;

        //Set agent' start position
        currentRoom = startRoom;
        transform.position = currentRoom.transform.position;

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        Debug.Log("Enter loop");
        while (true)
        {
            Debug.Log($"Cycle #{CycleCount}");

            Scan();
            UpdateMyStateBDI();

            List<Action> actionPlan = CreateActionPlan();

            yield return ExecuteActionPlan(actionPlan);

            CycleCount++;

            //Loop breaking condition
            if (CycleCount > 15)
            {
                Debug.Log("Exiting loop");
                yield break;
            }
        }
    }

    private void Scan()
    {
        perception = gameManager.environment.RoomList;
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
        actionPlan.Add(new Move(Direction.Right));
        actionPlan.Add(new Move(Direction.Up));
        actionPlan.Add(new Move(Direction.Right));
        actionPlan.Add(new Move(Direction.Left));
        actionPlan.Add(new Collect());
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
