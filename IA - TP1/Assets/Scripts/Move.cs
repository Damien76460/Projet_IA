using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action
{
    public Move(Room room) : base(room) { }

    public override IEnumerator ExecuteAction(Agent agent)
    {
        base.ExecuteAction(agent);
        Debug.Log($"{agent} moving to {room}");

        float timer = 0f;
        Vector3 originPos = agent.currentRoom.transform.position;
        Vector3 targetPos = room.transform.position;
        float duration = Vector3.Distance(originPos, targetPos) / agent.speed;
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float percent = Mathf.Clamp01(timer / duration);

            agent.transform.position = Vector3.Lerp(originPos, targetPos, percent);

            yield return null;
        }
        yield return null;
        agent.currentRoom = room;
    }
}