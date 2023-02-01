using System.Collections;
using UnityEngine;

/// <summary>
/// Generic class that handle Movement and dash in an entity
/// </summary>
public class EntityMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    private Vector3 velocity;

    public bool canMove = true;

    private Rigidbody myRb;
    private MovementAnimation myAnimation;


    //Variable used for Dash
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Dash variables")]
    [SerializeField] private float dashingSpeed = 6f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;
    private Vector2 dashDir;

    #region Getter
    public Vector2 Direction
    {
        get { return direction; }
    }

    #endregion

    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myAnimation = GetComponentInChildren<MovementAnimation>();

    }

    private void OnEnable()
    {
        InputController.instance.SpaceDown += StartDash;
    }

    private void OnDisable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.SpaceDown -= StartDash;
        }
    }

    /// <summary>
    /// Moving Entity in FixedUpdate to match the physics oh linked RigidBody
    /// </summary>
    void FixedUpdate()
    {
        //If in dash or in attack made the canMove value to be false
        if (canMove)
        {
            direction = InputController.instance.LeftStickDir;
            //If direction is none make RigidBody velocity to be 0
            if (direction == Vector2.zero) 
            {                
                myRb.velocity += -(myRb.velocity);
                myAnimation.SetDirection(direction);
            }
            else
            {
                //Generate 3D Vector from a 2D Vector
                velocity = new Vector3(InputController.instance.LeftStickDir.normalized.x * speed * Time.fixedDeltaTime, 0,
                                       InputController.instance.LeftStickDir.normalized.y * speed * Time.fixedDeltaTime);
                myRb.velocity = velocity;
                //Call Animation class to play animation
                myAnimation.SetDirection(direction);
            }
        }
        //If isDashing has become true since the last frame
        else if (isDashing)
        {
            //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
            velocity = new Vector3(dashDir.normalized.x * (speed * dashingSpeed) * Time.fixedDeltaTime, 0,
                                   dashDir.normalized.y * (speed * dashingSpeed) * Time.fixedDeltaTime);

            myRb.velocity = velocity;
        }
        //If no action is permitted to the entity made decay his RigidBody velocity quickly to 0
        //That could happen in a CoolDown Phase of an action or its execution, that prevent the entity to continue moving with the previous
        //velocity value
        else { myRb.velocity += -(myRb.velocity); }

    }


    /// <summary>
    /// Dash coroutine with 2 Time delay, the first set the dash duration in wich the player is locked sprinting in the givend direction,
    /// the second is for the coolDown of the action
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dash()
    {
        myAnimation.SetDirection(dashDir);
        canMove = false;
        canDash = false;
        isDashing = true;

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        canMove = true;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    /// <summary>
    /// Public function to call the Dash, do nothing if already dashing
    /// </summary>
    public void StartDash()
    {
        if (canDash)
        {
            dashDir = InputController.instance.LeftStickDir;
            StartCoroutine(Dash());
        }
    }
}
