using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action
{
    public Direction direction;
    public Move(Direction direction)
    {
        this.direction = direction;
    }

    public override IEnumerator ExecuteAction(Agent agent)
    {
        Debug.Log($"{agent} moving {direction}");

        float timer = 0f;
        if (agent.currentRoom.directedNeighbors.TryGetValue(direction, out Room targetRoom))
        {
            Debug.Log(targetRoom);
        }
        else
        {
            Debug.Log("No room in this direction");
        }
        
        Debug.Log(targetRoom);
        Vector3 originPos = agent.currentRoom.transform.position;
        Vector3 targetPos = targetRoom.transform.position;
        float duration = Vector3.Distance(originPos, targetPos) / agent.speed;
        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float percent = Mathf.Clamp01(timer / duration);

            agent.transform.position = Vector3.Lerp(originPos, targetPos, percent);

            yield return null;
        }
        agent.currentRoom = targetRoom;
        agent.transform.position = targetPos;
    }
}