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

    //Function that will be executed as a Thread for Agent behavior
    IEnumerator Loop()
    {
        while (true)
        {
            Scan();
            UpdateMyStateBDI();

            List<Action> actionPlan = CreateActionPlan();
            gameManager.UIManager.EnqueueActionUI(actionPlan);

            yield return ExecuteActionPlan(actionPlan);

            CycleCount++;

            //Loop breaking condition
            /*if (CycleCount > 15)
            {
                Debug.Log("Exiting loop");
                yield break;
            }*/
        }
    }

    //Agent sensors
    private void Scan()
    {
        perception = gameManager.environment.RoomList;
    }

    //Modify Agent's beliefs depending on what sensors perceive
    void UpdateBelief()
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
    
    //Modify Agent's desires depending on current belief
    void UpdateDesire()
    {
        desires.Clear();
        AddSpecificObject(beliefs, desires, "jewel");
        AddSpecificObject(beliefs, desires, "dust");
        AddSpecificObject(beliefs, desires, "both");
    }

    //Will spawn an object if it doesn't exist yet
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

    //Function that handle the Agent Mental State BDI
    void UpdateMyStateBDI()
    {
        UpdateBelief();
        UpdateDesire();
    }

    private List<Action> CreateActionPlan()
    {
        Navigation nav = new Navigation();
        List<Action> actionPlan = new List<Action>();
        for (int i = 0; i < desires.Count; i++)
        {
            List<Room> roomToVisit;
            roomToVisit = nav.BFS( i==0 ? currentRoom : desires[i -1], desires[i], gameManager.environment);

            List<Action> pathActionPlan = CreateActionPlanFromPath(roomToVisit);
            actionPlan.AddRange(pathActionPlan);
            
        }
        return actionPlan;
    }

    private List<Action> CreateActionPlanFromPath(List<Room> rooms)
    {
        List<Action> actionPlan = new List<Action>();
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].GetComponent<MeshRenderer>().material.color = Color.red;
            //if agent is at the desired room,
            if (i == rooms.Count - 1)
            {
                //we create a collect action
                actionPlan.Add(new Collect());
            }
            else
            {
                //we look at each of its neighbors,
                foreach (var neighbor in rooms[i].neighbors)
                {
                    if (rooms[i + 1] == neighbor)
                    {
                        //we create a move action with the associated direction
                        Direction directionToGo = rooms[i].neighborDirection[rooms[i + 1]];
                        actionPlan.Add(new Move(directionToGo));
                    }
                }
            }
        }
        return actionPlan;
    }

    private IEnumerator ExecuteActionPlan(List<Action> actionPlan)
    {
        foreach (var action in actionPlan)
        {
            yield return action.ExecuteAction(this);
            gameManager.UIManager.DequeueAction(0);
        }
    }
}
