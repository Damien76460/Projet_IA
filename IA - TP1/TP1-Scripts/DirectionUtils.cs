using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUtils
{
    public static Direction GetOppositeDirection(Direction direction)
    {
        Direction oppositeDirection;
        if ((int)direction - 2 < 0)
        {
            oppositeDirection = (Direction)3 - ((int)direction - 1);
        }
        else oppositeDirection = direction - 2;
        return oppositeDirection;
    }

    public static Dictionary<Vector2, Direction> Vector2ToDirection = new Dictionary<Vector2, Direction>
    {
        {Vector2.up, Direction.Up},
        {Vector2.down, Direction.Down},
        {Vector2.left, Direction.Left},
        {Vector2.right, Direction.Right}
    };

    public static Dictionary<Vector3, Direction> Vector3ToDirection = new Dictionary<Vector3, Direction>
    {
        {Vector3.forward, Direction.Up},
        {Vector3.back, Direction.Down},
        {Vector3.left, Direction.Left},
        {Vector3.right, Direction.Right}
    };

    public static Dictionary<Direction, Vector2> DirectionToVector2 = new Dictionary<Direction, Vector2>
    {
        {Direction.Up, Vector2.up},
        {Direction.Down, Vector2.down},
        {Direction.Left, Vector2.left},
        {Direction.Right, Vector2.right}
    };

    public static Dictionary<Direction, Vector3> DirectionToVector3 = new Dictionary<Direction, Vector3>
    {
        {Direction.Up, Vector3.forward},
        {Direction.Down, Vector3.back},
        {Direction.Left, Vector3.left},
        {Direction.Right, Vector3.right}
    };
}

