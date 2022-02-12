using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action 
{
    protected Room room;
    public Action(Room room)
    {
        this.room = room;
    }

    public virtual IEnumerator ExecuteAction(Agent agent)
    {
        if (room == null || agent == null)
        {
            yield break;
        }
    }
}