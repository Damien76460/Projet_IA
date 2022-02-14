using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : Action
{
    public Collect() { }

    public override IEnumerator ExecuteAction(Agent agent)
    {
        Room room = agent.currentRoom;
        yield return new WaitForSeconds(0.5f);

        //Collect room items
        if (room.jewel != null && room.dust != null)
        {
            Debug.Log($"{agent} has cleaned dust with a jewel in {room}");
            agent.DustCleaned++;
            Object.Destroy(room.dust.gameObject);
            Object.Destroy(room.jewel.gameObject);
        }
        else if (room.jewel != null)
        {
            Debug.Log($"{agent} has collected a jewel in {room}");
            agent.JewelCollected++;
            Object.Destroy(room.jewel.gameObject);
        }
        else if (room.dust != null)
        {
            Debug.Log($"{agent} has cleaned dust in {room}");
            agent.DustCleaned++;
            Object.Destroy(room.dust.gameObject);
        }
        else
        {
            Debug.Log($"{agent} has nothing to collect in {room}");
        }
        Debug.Log($"Current room {agent.currentRoom}");
    }

    public override string ToString()
    {
        return "Vacuum";
    }
}

