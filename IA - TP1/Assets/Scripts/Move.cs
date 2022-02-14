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
        float timer = 0f;
        if (!agent.currentRoom.directedNeighbor.TryGetValue(direction, out Room targetRoom))
        {
            yield break;
        }
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


    public override string ToString()
    {
        switch (direction)
        {
            case Direction.Up:
                return "Move up";
            case Direction.Left:
                return "Move left";
            case Direction.Down:
                return "Move down";
            case Direction.Right:
                return "Move right";
            default:
                return "Error";
        }
    }
}