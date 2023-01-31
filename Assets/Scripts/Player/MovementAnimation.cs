using UnityEngine;

//Enum to indicate al 8 directions in an isometric game
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

/// <summary>
/// Class that animate the player
/// </summary>
public class MovementAnimation : MonoBehaviour
{
    private Animator myAnimator;
    private EntityMovement playerMov;

    //Using string prefix to help calling the right animations by name
    string animPrefix = "Idle_";
    Direction lastDirection;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerMov = GetComponentInParent<EntityMovement>();
    }

    /// <summary>
    ///Detect if player is moving and detect the direction, then call the animation
    /// </summary>
    /// <param name="direction">Direction of the entity to animate</param>
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

    /// <summary>
    /// Caluclate the direction as Enum Direction in which the entity is moving
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Direction DirectionIndex(Vector2 direction)
    {
        Vector2 norDir = direction.normalized;
       
        float step = 360 / 8;  //Decide the 8 step of an isometic movement
        float offset = step / 2; //An offset to be sure never to go under 0
        float angle = Vector2.SignedAngle(Vector2.up, norDir); //Using Vector2.up as reference calculate the angle between him and entity movement dir

        //angle += offset;
        if(angle < 0) //Add 360 to the negative angles to mantain a range 0-360
        {
            angle += 360;
        }
        float stepCount = angle / step; // Calculate in which step we are
        return (Direction)Mathf.FloorToInt(stepCount); 

    }

    public void AttackAnimation(Vector2 direction)
    {
        myAnimator.Play("Atk_" + DirectionIndex(direction).ToString());
    }
}
