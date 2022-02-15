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

    public int itemNeededToStartInformedSearch = 10;

    private int cycleCount = 0;
    private int jewelCollected = 0;
    private int dustCleaned = 0;

    private bool informedSearch = false;

    private GameManager gameManager;

    public int CycleCount
    {
        get => cycleCount; 
        set
        {
            if (InformedSearch || beliefs.Count != 0)
            {
                cycleCount = value;
                gameManager.UIManager.iterationText.text = $"ITERATION : {cycleCount}";
            }
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

    public bool InformedSearch
    { 
        get => informedSearch;
        set
        {
            informedSearch = value;
            gameManager.UIManager.UpdateSearchText(informedSearch);
        }
    }

    public GameManager GameManager { get => gameManager; }

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

            yield return ExecuteActionPlan(actionPlan);

            CycleCount++;
        }
    }

    //Agent sensors
    void Scan()
    {
        perception = gameManager.environment.RoomList;
        gameManager.UIManager.UpdatePerceptionUI(perception);
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

        if (beliefs.Count == 0 && InformedSearch)
        {
            gameManager.StopThread();
        }

        if (jewelCollected + dustCleaned > itemNeededToStartInformedSearch && !InformedSearch)
        {
            InformedSearch = true;
        }
    }

    //Function that creates the action plan for every desire
    private List<Action> CreateActionPlan()
    {
        Navigation nav = new Navigation();
        List<Action> actionPlan = new List<Action>();
        for (int i = 0; i < desires.Count; i++)
        {
            List<Room> roomToVisit;
            if (informedSearch)
            {
                roomToVisit = nav.A_Star(i == 0 ? currentRoom : desires[i - 1], desires[i], gameManager.environment);
            }
            else
            {
                roomToVisit = nav.BFS(i == 0 ? currentRoom : desires[i - 1], desires[i], gameManager.environment);
            }
            
            List<Action> pathActionPlan = CreateActionPlanFromPath(roomToVisit);
            actionPlan.AddRange(pathActionPlan);
            
        }
        return actionPlan;
    }

    //Function that create the action plan for one desire
    private List<Action> CreateActionPlanFromPath(List<Room> rooms)
    {
        List<Action> actionPlan = new List<Action>();
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == rooms.Count - 1)
            {
                actionPlan.Add(new Collect());
            }
            else
            {
                foreach (var neighbor in rooms[i].neighbors)
                {
                    if (rooms[i + 1] == neighbor)
                    {
                        Direction directionToGo = rooms[i].neighborDirection[rooms[i + 1]];
                        actionPlan.Add(new Move(directionToGo));
                    }
                }
            }
        }
        gameManager.UIManager.EnqueueActionUI(actionPlan);
        return actionPlan;
    }

    //Agent's effector
    private IEnumerator ExecuteActionPlan(List<Action> actionPlan)
    {
        foreach (var action in actionPlan)
        {
            yield return action.ExecuteAction(this);
            gameManager.UIManager.DequeueAction(0);
        }
    }
}
