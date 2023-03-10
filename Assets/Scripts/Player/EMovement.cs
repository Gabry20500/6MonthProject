using System.Collections;
using UnityEngine;

/// <summary>
/// Generic class that handle Movement and dash in an entity
/// </summary>
public class EMovement : MonoBehaviour, IHittable, IClashable
{
    [Header("Movement variables")]
    private bool canMove = true;
    [SerializeField] public float move_Speed;
    [SerializeField] private Vector2 move_Dir;
    private Vector3 move_Vel;


    private Rigidbody mov_RigidB;
    private EAnimator mov_Animator;
    private SpriteRenderer body_Renderer;
    private Sword player_Sword;

    [Header("Dash variables")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float dash_Speed = 6f;
    [SerializeField] private float dash_Time = 0.5f;
    [SerializeField] private float dash_Cooldown = 1f;
    private Vector2 dash_Dir;

    [Header("Attack variables")]
    [SerializeField] private bool isAttacking = false;
     private bool isVulnerable = true;

    [Header("Dameged frames variables")]
    [SerializeField] private float inv_Time = 1.0f;
    [SerializeField] private float inv_Flash_Tick = 0.1f;
    [SerializeField] Color inv_Color = Color.clear;
    private Color color_Buff;
    private Color color_To;
    private Color init_Color;

    #region Getter
    public Rigidbody Rigidbody
    {
        get { return mov_RigidB; }
    }
    public Vector2 Direction
    {
        get { return move_Dir; }
    }
    public Vector3 Velocity
    {
        get { return move_Vel; }
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
        mov_RigidB = GetComponent<Rigidbody>();
        mov_Animator = GetComponentInChildren<EAnimator>();
        body_Renderer = mov_Animator.gameObject.GetComponent<SpriteRenderer>();
        player_Sword = GetComponentInChildren<Sword>();

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
            move_Dir = InputController.instance.LeftStickDir;
            //If direction is none make RigidBody velocity to be 0
            if (move_Dir == Vector2.zero)
            {
                mov_RigidB.velocity += -(mov_RigidB.velocity);
                mov_Animator.SetDirection(move_Dir);
            }
            else
            {
                //Generate 3D Vector from a 2D Vector
                move_Vel = new Vector3(InputController.instance.LeftStickDir.normalized.x * move_Speed * Time.fixedDeltaTime, 0,
                                       InputController.instance.LeftStickDir.normalized.y * move_Speed * Time.fixedDeltaTime);
                mov_RigidB.velocity = move_Vel;
                //Call Animation class to play animation
                mov_Animator.SetDirection(move_Dir);
            }
        }
        //If isDashing has become true since the last frame
        else if (isDashing)
        {
            //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
            move_Vel = new Vector3(dash_Dir.normalized.x * (move_Speed * dash_Speed) * Time.fixedDeltaTime, 0,
                                   dash_Dir.normalized.y * (move_Speed * dash_Speed) * Time.fixedDeltaTime);

            mov_RigidB.velocity = move_Vel;
        }       
        //If no action is permitted to the entity made decay his RigidBody velocity quickly to 0
        //That could happen in a CoolDown Phase of an action or its execution, that prevent the entity to continue moving with the previous
        //velocity value
        else if(isAttacking == false)
        { mov_RigidB.velocity += -(mov_RigidB.velocity); }
    }


    /// <summary>
    /// Dash coroutine with 2 Time delay, the first set the dash duration in wich the player is locked sprinting in the givend direction,
    /// the second is for the coolDown of the action
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCoroutine()
    {
        mov_Animator.SetDirection(dash_Dir);
        canMove = false;
        canDash = false; 
        isDashing = true;

        yield return new WaitForSeconds(dash_Time);
        isDashing = false;
        canMove = true;

        yield return new WaitForSeconds(dash_Cooldown);
        canDash = true;
    }


    /// <summary>
    /// Public function to call the Dash, do nothing if already dashing
    /// </summary>
    public void Dash()
    {
        if (canDash)
        {
            dash_Dir = InputController.instance.LeftStickDir;
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator KnockBackCoroutine(Vector3 knockDir, EnemyData enemy)
    {
        float buffer = 0;

        //canMove = false;
        canDash = false;
        isDashing = false;
        isAttacking = false;

        while (buffer < enemy.knockDuration)
        {
            transform.position += knockDir * enemy.knockSpeed * Time.fixedDeltaTime;
            buffer += Time.fixedDeltaTime;
            yield return null;
        }

        canMove = true;
        canDash = true;
        player_Sword.canRotate = true;
        InputController.instance.LeftMouseDown += player_Sword.Swing;//Re-inscribe swing to InputController
    }

    /// <summary>
    /// Function called when player is hitted by something that cuold or not make damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="knockBackDir"></param>
    /// <param name="enemy"></param>
    public void OnHit(Vector3 knockBackDir, EnemyData enemy, float damage = 0.0f)
    {
        if (isVulnerable == true)
        {
            if (damage != 0.0f)
            {
                GetComponent<Entity>().TakeDamage(damage);
                StartCoroutine(InvincibilityCoroutine());
            }

            StartCoroutine(KnockBackCoroutine(knockBackDir, enemy));
        }
    }



    private IEnumerator InvincibilityCoroutine()
    {
        isVulnerable = false;

        color_To = inv_Color;
        init_Color = body_Renderer.color;
        int i = 1;
        float buff = 0.0f;
        while (buff < inv_Time)
        {
            if (buff > inv_Flash_Tick * i)
            {
                color_Buff = body_Renderer.material.color;
                body_Renderer.material.color = color_To;
                color_To = color_Buff;
                i = i + 1;
            }
            yield return null;
            buff += Time.deltaTime;
        }
        body_Renderer.material.color = init_Color;

        isVulnerable = true;

    }
}
