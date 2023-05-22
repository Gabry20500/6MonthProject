using System.Collections;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// Generic class that handle Movement and dash in an entity
/// </summary>
public class EMovement : MonoBehaviour, IHittable, IClashable
{
    public PlayerData self;

    [Header("Movement variables")]
    [SerializeField] private bool canMove = true;
    private Vector2 move_Dir;
    private Vector3 move_Dir3D;
    private Vector3 move_Vel;
    private Vector3 temp_Acc;
    private Vector3 temp_Dec;

    [Header("Dash variables")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    private Vector3 dash_Dir;
    private RaycastHit hit;
    private Ray ray;

    [Header("Attack variables")]
    [SerializeField] private bool isAttacking = false;
     private bool isVulnerable = true;

    [Header("Dameged frames variables")]
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
        get { return self.acceleration; }
    }
    public float Acceleration
    {
        get { return self.move_Max_Speed; }
    }
    public float Deceleration
    {
        get { return self.deceleration; }
    }
    public float Dash_Speed
    {
        get { return self.dash_Speed; }
    }
    public float Dash_Time
    {
        get { return self.dash_Time; }
    }
    public float Dash_Cooldown
    {
        get { return self.dash_Cooldown; }
    }
    public float Inv_Time
    {
        get { return self.inv_Time; }
    }
    public float Inv_Flash_Tick
    {
        get { return self.inv_Flash_Tick; }
    }
    public Color Inv_Color
    {
        get { return self.inv_Color; }
    }

    public Player Player
    {
        get { return player; }
    }

    public Sword Sword { get => player_Sword; } 
    #endregion

    private void Awake()
    {
        mov_RigidB = GetComponent<Rigidbody>();
        mov_Animator = GetComponentInChildren<EAnimator>();
        body_Renderer = mov_Animator.gameObject.GetComponent<SpriteRenderer>();
        player_Sword = GetComponentInChildren<Sword>();
        player = GetComponent<Player>();
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
                Decelerate();

                move_Dir3D = player_Sword.transform.position - transform.position;
                move_Dir = new Vector2(move_Dir3D.x, move_Dir3D.z).normalized;
                mov_Animator.SetDirection(move_Dir, true);
            }
            else
            {
                Accelerate();
                //Call Animation class to play animation
                mov_Animator.SetDirection(move_Dir);
            }
        }  
        //If no action is permitted to the entity made decay his RigidBody velocity quickly to 0
        //That could happen in a CoolDown Phase of an action or its execution, that prevent the entity to continue moving with the previous
        //velocity value
        else if(isAttacking == false && IsDashing == false)
        {
            Decelerate();
        }
    }


    private void Accelerate()
    {
        //Generate 3D Vector from a 2D Vector
        move_Dir3D = new Vector3(InputController.instance.LeftStickDir.normalized.x, 0, InputController.instance.LeftStickDir.normalized.y);
        if( (mov_RigidB.velocity + (move_Dir3D * (self.acceleration * Time.fixedDeltaTime))).magnitude > self.move_Max_Speed)
        {
            temp_Acc = (mov_RigidB.velocity + (move_Dir3D * (self.acceleration * Time.fixedDeltaTime))).normalized;
            temp_Acc *= self.move_Max_Speed;
            mov_RigidB.velocity = temp_Acc;
        }
        else
        {
            mov_RigidB.velocity += (move_Dir3D * (self.acceleration * Time.fixedDeltaTime));
        }
    }

    private void Decelerate()
    {
        //if (mov_RigidB.velocity != Vector3.zero)
        //{
        //    if ( Mathf.Abs((mov_RigidB.velocity - (mov_RigidB.velocity.normalized * (self.deceleration * Time.fixedDeltaTime))).magnitude) > 0.5 )
        //    {
        //        mov_RigidB.velocity -= (mov_RigidB.velocity.normalized * (self.deceleration * Time.fixedDeltaTime));
        //    }
        //   else
        //    {
                mov_RigidB.velocity = Vector3.zero;
        //    }
        //}
    }


    /// <summary>
    /// Public function to call the Dash, do nothing if already dashing
    /// </summary>
    public void Dash()
    {
        if (canDash)
        {
            dash_Dir = new Vector3(InputController.instance.LeftStickDir.x, 0.0f, InputController.instance.LeftStickDir.y).normalized;
           
            ray = new Ray(transform.position, dash_Dir * self.dash_Speed);
            if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Wall")))
            {
                StartCoroutine(DashCoroutine(new Vector3(hit.point.x, transform.position.y, hit.point.z)));
            }
            else
            {
                StartCoroutine(DashCoroutine(transform.position + dash_Dir * self.dash_Speed));
            }
        }
    }

    /// <summary>
    /// Dash coroutine with 2 Time delay, the first set the dash duration in wich the player is locked sprinting in the givend direction,
    /// the second is for the coolDown of the action
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashCoroutine(Vector3 destination)
    {
        mov_Animator.SetDirection(dash_Dir);
        canMove = false;
        canDash = false; 
        isDashing = true;
        player.dashBar.value = 0.0f;
        yield return transform.DOMove(destination, self.dash_Time).SetEase(self.dash_Ease).WaitForCompletion();
        canMove = true;
        isDashing = false;

        StartCoroutine(player.Dash_Bar_Cooldown(self.dash_Cooldown));
        yield return new WaitForSeconds(self.dash_Cooldown);
        canDash = true;
    }

    private IEnumerator KnockBackCoroutine(Vector3 knock_Dir, EnemyData enemy, Ease ease)
    {
        //canMove = false;
        canDash = false;
        isDashing = false;
        isAttacking = false;

        ray = new Ray(transform.position, knock_Dir * enemy.knockSpeed);
        if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Wall")))
        {
            yield return transform.DOMove(new Vector3(hit.point.x, transform.position.y, hit.point.z), self.dash_Time).SetEase(ease).WaitForCompletion();
        }
        else
        {
            yield return transform.DOMove(transform.position + knock_Dir * enemy.knockSpeed, self.dash_Time).SetEase(ease).WaitForCompletion();
        }

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
                StartCoroutine(KnockBackCoroutine(knockBackDir, enemy, self.knock_Ease));
            }
            else
            {
                StartCoroutine(KnockBackCoroutine(knockBackDir, enemy, self.clash_Ease));
            } 
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isVulnerable = false;

        color_To = self.inv_Color;
        init_Color = body_Renderer.color;
        int i = 1;
        float buff = 0.0f;
        while (buff < self.inv_Time)
        {
            if (buff > self.inv_Flash_Tick * i)
            {
                color_Buff = body_Renderer.material.color;
                body_Renderer.material.color = color_To;
                color_To = color_Buff;
                i++;
            }
            yield return null;
            buff += Time.deltaTime;
        }
        body_Renderer.material.color = init_Color;
        isVulnerable = true;
    }
}
