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
/// Class that animate the any entity in a isometric 3D world with a sprite character with 8 possible direction of moving
/// </summary>
public class EAnimator : MonoBehaviour
{
    /// <summary>
    /// Reference to the needed Animator to animate
    /// </summary>
    protected Animator _animator;
    /// <summary>
    /// Reference to the Movement class that elaborates and pass the movement value
    /// </summary>
    private EMovement _movement;

    /// <summary>
    ///Using string prefix to help calling the right animations by name
    /// </summary>
    protected string prefix = "Idle_";
    protected Direction lastDir;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponentInParent<EMovement>();
    }

    /// <summary>
    ///Detect if player is moving and detect the direction, then call the animation
    /// </summary>
    /// <param name="direction">Direction of the entity to animate</param>
    public void SetDirection( Vector2 direction )
    {
        if(direction.magnitude < 0.01f)
        {
            prefix = "Idle_";
        }
        else
        {
            prefix = "Run_";
            lastDir = DirectionIndex(direction);
        }
        _animator.Play(prefix + lastDir.ToString());
    }

    /// <summary>
    /// Caluclate the direction as Enum Direction in which the entity is moving
    /// </summary>
    /// <param name="direction">The direction as 2D Vector of the entity movement</param>
    /// <returns></returns>
    protected Direction DirectionIndex(Vector2 direction)
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

    /// <summary>
    /// Activate the Attack animation based on the given direction
    /// </summary>
    /// <param name="direction"></param>
    //public void AttackAnimation(Vector2 direction)
    //{
    //    _animator.Play("Atk_" + DirectionIndex(direction).ToString());   
    //}
}
