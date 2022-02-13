using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Environment environment;
    public Agent agent;
    public UIManager UIManager;

    private void Start()
    {
        environment.InitEnvironment();
        agent.InitAgent(this, environment.RoomList[0]);
    }
}
