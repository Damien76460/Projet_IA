using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Environment environment;
    public Agent agent;

    private void Start()
    {
        environment.InitEnvironment();
        agent.InitAgent();
    }
}
