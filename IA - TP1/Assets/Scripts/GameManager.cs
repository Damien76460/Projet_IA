using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Environment environment;
    public Agent agent;
    public UIManager UIManager;

    //Call Agent and Environment Threads to start them
    private void Start()
    {
        environment.InitEnvironment();
        agent.InitAgent(this, environment.RoomList[12]);
    }

    public void StopThread()
    {
        environment.StopAllCoroutines();
        agent.StopAllCoroutines();
    }
}
