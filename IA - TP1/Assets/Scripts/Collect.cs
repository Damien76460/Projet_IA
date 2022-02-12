using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : Action
{
    public Collect(Room room) : base(room) { }

    public override IEnumerator ExecuteAction(Agent agent)
    {
        base.ExecuteAction(agent);

        yield return null;

        //Collect room items
        if (room.jewel != null && room.dust != null)
        {
            Debug.Log($"{agent} has cleaned dust with a jewel in {room}");
            agent.dustCleaned++;
            Object.Destroy(room.dust.gameObject);
            Object.Destroy(room.jewel.gameObject);
        }
        else if (room.jewel != null)
        {
            Debug.Log($"{agent} has collected a jewel in {room}");
            agent.jewelCollected++;
            Object.Destroy(room.jewel.gameObject);
        }
        else if (room.dust != null)
        {
            Debug.Log($"{agent} has cleaned dust in {room}");
            agent.dustCleaned++;
            Object.Destroy(room.dust.gameObject);
        }
        else
        {
            Debug.Log($"{agent} has nothing to collect in {room}");
        }

       
    }
}

