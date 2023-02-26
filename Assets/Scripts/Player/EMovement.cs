using System.Collections;
using UnityEngine;

/// <summary>
/// Generic class that handle Movement and dash in an entity
/// </summary>
public class EMovement : MonoBehaviour, IHittable
{
    [Header("Movement variables")]
    [SerializeField] public float speed;
    [SerializeField] private Vector2 direction;
    private Vector3 velocity;

    private bool canMove = true;

    private Rigidbody _rigidBody;
    private EAnimator _animator;

    [Header("Dash variables")]
    //Variable used for Dash
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float dashingSpeed = 6f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;
    private Vector2 dashDir;

    [Header("Attack variables")]
    [SerializeField] private bool isAttacking = false;

    #region Getter
    public Rigidbody Rigidbody
    {
        get { return _rigidBody; }
    }
    public Vector2 Direction
    {
        get { return direction; }
    }
    public Vector3 Velocity
    {
        get { return velocity; }
    }
    public ref bool CanMove
    {
        get { return ref canMove; }
    }
    public ref bool IsDashing
    {
        get
        {
            return ref isDashing;
        }
    }
    public ref bool CanDash
    {
        get
        {
            return ref canDash;
        }
    }
    public ref bool IsAttacking
    {
        get
        {
            return ref isAttacking;
        }
    }

    #endregion

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<EAnimator>();

    }

    private void Start()
    {
        InputController.instance.SpaceDown += Dash;
    }

    private void OnDisable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.SpaceDown -= Dash;
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
                _rigidBody.velocity += -(_rigidBody.velocity);
                _animator.SetDirection(direction);
            }
            else
            {
                //Generate 3D Vector from a 2D Vector
                velocity = new Vector3(InputController.instance.LeftStickDir.normalized.x * speed * Time.fixedDeltaTime, 0,
                                       InputController.instance.LeftStickDir.normalized.y * speed * Time.fixedDeltaTime);
                _rigidBody.velocity = velocity;
                //Call Animation class to play animation
                _animator.SetDirection(direction);
            }
        }
        //If isDashing has become true since the last frame
        else if (isDashing)
        {
            //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
            velocity = new Vector3(dashDir.normalized.x * (speed * dashingSpeed) * Time.fixedDeltaTime, 0,
                                   dashDir.normalized.y * (speed * dashingSpeed) * Time.fixedDeltaTime);

            _rigidBody.velocity = velocity;
        }       
        //If no action is permitted to the entity made decay his RigidBody velocity quickly to 0
        //That could happen in a CoolDown Phase of an action or its execution, that prevent the entity to continue moving with the previous
        //velocity value
        else if(isAttacking == false)
        { _rigidBody.velocity += -(_rigidBody.velocity); }
    }


    /// <summary>
    /// Dash coroutine with 2 Time delay, the first set the dash duration in wich the player is locked sprinting in the givend direction,
    /// the second is for the coolDown of the action
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCoroutine()
    {
        _animator.SetDirection(dashDir);
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
    public void Dash()
    {
        if (canDash)
        {
            dashDir = InputController.instance.LeftStickDir;
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator KnockBackCoroutine(Vector3 knockBackDir, EnemyData enemy)
    {
        Debug.Log("Player hotted");
        float buffer = 0;

        //canMove = false;
        canDash = false;
        isDashing = false;
        isAttacking = false;


        while (buffer < enemy.knockBackDuration)
        {
            transform.position += knockBackDir * enemy.knockBackSpeed * Time.fixedDeltaTime;
            buffer += Time.fixedDeltaTime;
            yield return null;
        }

        canMove = true;
        canDash = true;
        GetComponentInChildren<Sword>().canRotate = true;
        InputController.instance.LeftMouseDown += GetComponentInChildren<Sword>().Swing;//Re-inscribe swing to InputController
    }
    public void OnHit(float damage, Vector3 knockBackDir, EnemyData enemy)
    {

        GetComponent<Entity>().TakeDamage(damage);
        //Elaborate all and start knock back coroutine
        StartCoroutine(KnockBackCoroutine(knockBackDir, enemy));
    }

    public void OnClash(Vector3 knockBackDir, EnemyData enemy)
    {
        StopAllCoroutines();
        GetComponentInChildren<Sword>().StopAllCoroutines();

        StartCoroutine(KnockBackCoroutine(knockBackDir, enemy));
    }
}
