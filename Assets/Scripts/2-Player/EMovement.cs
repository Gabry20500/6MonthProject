using System.Collections;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Generic class that handle Movement and dash in an entity
/// </summary>
public class EMovement : MonoBehaviour, IHittable, IClashable
{
    [Header("Movement variables")]
    [SerializeField] private bool canMove = true;
    private float move_Speed;
    private Vector2 move_Dir;
    private Vector3 move_Vel;

    [Header("Dash variables")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    private float dash_Speed;
    private float dash_Time;
    private float dash_Cooldown;
    private Vector3 dash_Dir;
    private Ease dash_Ease;
    private Ease knock_Ease;
    private Ease clash_Ease;

    [Header("Attack variables")]
    [SerializeField] private bool isAttacking = false;
     private bool isVulnerable = true;

    [Header("Dameged frames variables")]
    private float inv_Time;
    private float inv_Flash_Tick;
    private Color inv_Color;
    private Color color_Buff;
    private Color color_To;
    private Color init_Color;

    //Components references area
    private Rigidbody mov_RigidB;
    private EAnimator mov_Animator;
    private SpriteRenderer body_Renderer;
    private Sword player_Sword;
    private Player player;

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

    public float Move_Speed
    {
        get { return move_Speed; }
    }
    public float Dash_Speed
    {
        get { return dash_Speed; }
    }
    public float Dash_Time
    {
        get { return dash_Time; }
    }
    public float Dash_Cooldown
    {
        get { return dash_Cooldown; }
    }
    public float Inv_Time
    {
        get { return inv_Time; }
    }
    public float Inv_Flash_Tick
    {
        get { return inv_Flash_Tick; }
    }
    public Color Inv_Color
    {
        get { return inv_Color; }
    }
    #endregion

    private void Awake()
    {
        mov_RigidB = GetComponent<Rigidbody>();
        mov_Animator = GetComponentInChildren<EAnimator>();
        body_Renderer = mov_Animator.gameObject.GetComponent<SpriteRenderer>();
        player_Sword = GetComponentInChildren<Sword>();
        player = GetComponent<Player>();
    }

    public void InitParameters(PlayerData data)
    {
        move_Speed = data.move_Speed;
        dash_Speed = data.dash_Speed;
        dash_Time = data.dash_Time;
        dash_Cooldown = data.dash_Cooldown;
        dash_Ease = data.dash_Ease;
        knock_Ease = data.knock_Ease;
        clash_Ease = data.clash_Ease;
        inv_Time = data.inv_Time;
        inv_Flash_Tick = data.inv_Flash_Tick;
        inv_Color = data.inv_Color;
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
        //If no action is permitted to the entity made decay his RigidBody velocity quickly to 0
        //That could happen in a CoolDown Phase of an action or its execution, that prevent the entity to continue moving with the previous
        //velocity value
        else if(isAttacking == false && IsDashing == false)
        { mov_RigidB.velocity += -(mov_RigidB.velocity); }
    }


    /// <summary>
    /// Public function to call the Dash, do nothing if already dashing
    /// </summary>
    public void Dash()
    {
        if (canDash)
        {
            dash_Dir = new Vector3(InputController.instance.LeftStickDir.x, 0.0f, InputController.instance.LeftStickDir.y).normalized;
            StartCoroutine(DashCoroutine());
        }
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

        yield return transform.DOMove(transform.position + dash_Dir * dash_Speed, dash_Time).SetEase(dash_Ease).WaitForCompletion();
        isDashing = false;
        canMove = true;

        yield return new WaitForSeconds(dash_Cooldown);
        canDash = true;
    }




    private IEnumerator KnockBackCoroutine(Vector3 knock_Dir, EnemyData enemy, Ease ease)
    {
        //canMove = false;
        canDash = false;
        isDashing = false;
        isAttacking = false;

        yield return transform.DOMove(transform.position + knock_Dir * enemy.knockSpeed, dash_Time).SetEase(ease).WaitForCompletion();

        canMove = true;
        canDash = true;
        player_Sword.canRotate = true;
        InputController.instance.LeftMouseDown += player_Sword.LR_Swing;//Re-inscribe swing to InputController
        InputController.instance.RightMouseDown += player_Sword.RL_Swing;//Re-inscribe swing to InputController
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
                player.TakeDamage(damage);
                StartCoroutine(InvincibilityCoroutine());
                StartCoroutine(KnockBackCoroutine(knockBackDir, enemy, knock_Ease));
            }
            else
            {
                StartCoroutine(KnockBackCoroutine(knockBackDir, enemy, clash_Ease));
            } 
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
