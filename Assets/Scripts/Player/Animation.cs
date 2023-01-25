using System;
using UnityEngine;

public enum Direction
{
    N,
    NW,
    W,
    SW,
    S,
    SE,
    E,
    NE
}


public class Animation : MonoBehaviour
{
    private Animator myAnimator;
    private PlayerMovement playerMov;


    string animPrefix = "Idle_";
    Direction lastDirection;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerMov = GetComponentInParent<PlayerMovement>();
    }
    public void SetDirection( Vector2 direction)
    {
        if(direction.magnitude < 0.01f)
        {
            animPrefix = "Idle_";
        }
        else
        {
            animPrefix = "Run_";
            lastDirection = DirectionIndex(direction);
        }
        myAnimator.Play(animPrefix + lastDirection.ToString());
    }


    private Direction DirectionIndex(Vector2 direction)
    {
        Vector2 norDir = direction.normalized;

        float step = 360 / 8;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        //angle += offset;
        if(angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return (Direction)Mathf.FloorToInt(stepCount);

    }
}
