using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : Action
{
    public Collect() { }

    //Agent effector
    public override IEnumerator ExecuteAction(Agent agent)
    {
        Room room = agent.currentRoom;
        yield return new WaitForSeconds(0.5f);

        //Collect room items
        if (room.jewel != null && room.dust != null)
        {
            agent.DustCleaned++;
            GameObject dust = room.dust.gameObject;
            GameObject jewel = room.jewel.gameObject;
            room.dust = null;
            room.jewel = null;
            Object.Destroy(dust);
            Object.Destroy(jewel);
        }
        else if (room.jewel != null)
        {
            agent.JewelCollected++;
            GameObject jewel = room.jewel.gameObject;
            room.jewel = null;
            Object.Destroy(jewel);
        }
        else if (room.dust != null)
        {
            agent.DustCleaned++;
            GameObject dust = room.dust.gameObject;
            room.dust = null;
            Object.Destroy(dust);
        }
        else
        {

        }

        agent.GameManager.UIManager.UpdateSelectedPerceptionUI(agent.perception.IndexOf(room), room);
    }

    public override string ToString()
    {
        return "Vacuum";
    }
}

