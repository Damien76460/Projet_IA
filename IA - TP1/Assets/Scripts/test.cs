using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Environment env;

    [ContextMenu("Test nav")]
    void TestNav()
    {
        Navigation nav = new Navigation();
        nav.BFS(env.roomList[0]);
        
    }
}
