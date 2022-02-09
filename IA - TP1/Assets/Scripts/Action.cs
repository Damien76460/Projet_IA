using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action 
{
    public abstract void ExecuteAction(Room room);

}

public class Move : Action
{
    public override void ExecuteAction(Room room)
    {
        //Move agent to room

    }
}

public class Collect : Action
{
    public override void ExecuteAction(Room room)
    {
        //Collect room items
    }
}
