using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// SwordScript class that manage an item to act like a sword around his Y axis
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private Player_SwordSO swordSO;
    [SerializeField] public Player_SwordData swordData;


    [SerializeField] public bool canRotate = true;
    [SerializeField] bool swinging = false;

    #region MouseTracing
    private Vector2 mousePos;
    private Vector2 lastMousePos;
    private Ray ray; //Ray to detect the world poin hitted by the mouse position
    private RaycastHit hit;
    private Vector3 currentDir; //Real world 3D direction elaborated from the mouse-world hit position
    private Vector3 currentStickDir;
    private Vector3 lastStickDir;
    [SerializeField] private float deadOffSet = 5f;
    #endregion

    private EMovement player_Movement; //Link to the entity movement class to sto his movement and to perform other actions
    [SerializeField] private EAnimator e_Animator; //Animation class of the entity to link in editor to call the swing animation on the sprite
    [SerializeField] Transform rot_Pivot;

    #region SwingAnimation parameters
    private Vector3 initialDir;
    private Vector3 targetDir;
    private Quaternion targetRot;
    #endregion

    private TrailRenderer trail;
    private AudioSource sword_Audio;
    private Vector3 knockDir;

    public float Damage
    {
        get
        {
            return swordData.Damage;
        }
    }

    private void Awake()
    {
        swordData = new Player_SwordData(swordSO);
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
        sword_Audio = GetComponentInParent<AudioSource>();
        sword_Audio.clip = swordData.baseSwing;
        player_Movement = gameObject.GetComponentInParent<EMovement>();
        
    }

    /// <summary>
    /// Inscribe Swing function to desired event in InputController
    /// </summary>
    private void Start()
    {
        InputController.instance.LeftMouseDown += LR_Swing;
        InputController.instance.RightMouseDown += RL_Swing;
    }
    /// <summary>
    /// Unscribe Swing function to desired event in InputController
    /// </summary>
    private void OnDisable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.LeftMouseDown -= LR_Swing;
            InputController.instance.RightMouseDown -= RL_Swing;
        }
    }
    void Update()
    {
        /*Utilities to draw on editor the reach of the sword
          Debug.DrawLine(pivot.position, transform.forward * 10, Color.green);
          Vector3 rot = Quaternion.AngleAxis(-(swingWidth/2), Vector3.up) * pivot.forward;
          Debug.DrawLine(pivot.position, rot * 10, Color.red);
          rot = Quaternion.AngleAxis((swingWidth * 1.2f), Vector3.up) * pivot.forward;
          Debug.DrawLine(pivot.position, rot * 10, Color.red);*/

        if (canRotate)
        {
            //Rotate directly using the axis value from the right stick in gameoad
            if (InputController.instance.usingMouse == false)
            {
                currentStickDir = InputController.instance.RT_Stick_Dir;
                if (Mathf.Abs((currentStickDir - lastStickDir).magnitude) > deadOffSet)
                {
                    lastStickDir = currentStickDir;
                    currentDir = new Vector3(InputController.instance.RT_Stick_Dir.x, 0.0f, InputController.instance.RT_Stick_Dir.y);
                    rot_Pivot.forward = new Vector3(InputController.instance.RT_Stick_Dir.x, 0.0f, InputController.instance.RT_Stick_Dir.y);
                }
            }
            //More elaborations needed to obtain direction from mouse pointing the world
            else if (InputController.instance.usingMouse)
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Vector2 of the mouse position in the screen
                if (Mathf.Abs((mousePos - lastMousePos).magnitude) > deadOffSet)
                {
                    lastMousePos = mousePos;
                    ray = Camera.main.ScreenPointToRay(mousePos);//Generate a ray from the mouse pos in the direction of the world from the camera
                    Physics.Raycast(ray, out hit);//Detect collision
                    currentDir = (hit.point - rot_Pivot.position).normalized; //Calculate direction from the entity and the hitted world point
                    currentDir = new Vector3(currentDir.x, 0.0f, currentDir.z); //Generate a new 3D vector
                    rot_Pivot.forward = currentDir;//Set sword direction
                }
            }
        }
    }


    /// <summary>
    /// Swing public function to perform a swing
    /// </summary>
    public void RL_Swing()
    {
        if (swinging == false)
        {
            swinging = true;
            InputController.instance.LeftMouseDown -= LR_Swing;
            InputController.instance.RightMouseDown -= RL_Swing;
            player_Movement.CanMove = false; //The linked entity can't move
            canRotate = false; //The sword can no more rotate
            e_Animator.SetDirection(new Vector2(currentDir.x, currentDir.z)); //Put the player in the swing direction before everything else
            StartCoroutine(SwingAnimation(currentDir, 1)); //Starting the coroutine swing animation
        }
    }
    public void LR_Swing()
    {
        if (swinging == false)
        {
            swinging = true;
            InputController.instance.LeftMouseDown -= LR_Swing;
            InputController.instance.RightMouseDown -= RL_Swing;
            player_Movement.CanMove = false; //The linked entity can't move
            canRotate = false; //The sword can no more rotate
            e_Animator.SetDirection(new Vector2(currentDir.x, currentDir.z)); //Put the player in the swing direction before everything else
            StartCoroutine(SwingAnimation(currentDir, -1)); //Starting the coroutine swing animation
        }
    }



    
    private IEnumerator SwingAnimation(Vector3 swingDir, int i)
    {
        trail.enabled = true;

        //initialDir = Quaternion.AngleAxis(-(swordData.swingWidth / 2) * i, Vector3.up) * rot_Pivot.forward; //Calculcate the initial direction of the swing animation
        initialDir = rot_Pivot.forward;
         rot_Pivot.forward = initialDir;                                                                     //Set forward to the initial position of the animation
         //targetDir = Quaternion.AngleAxis((swordData.swingWidth * 1.2f) * i, Vector3.up) * rot_Pivot.forward;//Calculcate the desired final direction 
         targetDir = Quaternion.AngleAxis(swordData.swingWidth * i, Vector3.up) * rot_Pivot.forward;
         targetRot = Quaternion.LookRotation(targetDir);


        //Call the attack dash function in the entity attached to this script
        StartCoroutine(AtkDash(new Vector2(swingDir.x, swingDir.z), swordData.dashSpeed, swordData.dashDuration));

        sword_Audio.Play();
 
        yield return rot_Pivot.DOLocalRotateQuaternion(targetRot, swordData.swingSpeed).SetEase(swordData.swingEase).WaitForCompletion();

        trail.enabled = false;
        canRotate = true; //Enable sword movement
        player_Movement.CanMove = true;//Enable entity movment
        yield return new WaitForSeconds(swordData.swingCoolDown);
                               

        InputController.instance.LeftMouseDown += LR_Swing;//Re-inscribe swing to InputController
        InputController.instance.RightMouseDown += RL_Swing;
        swinging = false;
    }

    private Vector3 velocity;
    public IEnumerator AtkDash(Vector2 direction, float dashAtkSpeed, float duration)
    {
        player_Movement.CanMove = false;
        player_Movement.IsDashing = false;
        player_Movement.CanDash = false;
        player_Movement.IsAttacking = true;

        //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
        velocity = new Vector3(direction.normalized.x * (player_Movement.Move_Speed * dashAtkSpeed) * Time.fixedDeltaTime, 0,
                               direction.normalized.y * (player_Movement.Move_Speed * dashAtkSpeed) * Time.fixedDeltaTime);

         player_Movement.Rigidbody.velocity = velocity;
         yield return new WaitForSeconds(duration);

        player_Movement.IsAttacking = false;
        player_Movement.CanMove = true;
        player_Movement.CanDash = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && player_Movement.IsAttacking == true)
        {
            knockDir = Utils.CalculateDir(collision.gameObject.transform.position, transform.parent.position);
            StartCoroutine(Utils.FreezeFrames(swordData.freeze_Intensity, swordData.freeze_Duration));
            collision.gameObject.GetComponent<EnemyAI>().OnHit(Damage, knockDir, swordData);
            return;
        }
        if (collision.gameObject.CompareTag("EnemySword") && canRotate == false && collision.gameObject.GetComponentInParent<EnemyAI>().enemy_Animator.GetBool("Attack") == true)
        {
            //Passing knock back direction to applicate to te hitted entity
            knockDir = Utils.CalculateDir(transform.parent.position, collision.gameObject.transform.position);
            player_Movement.OnHit(knockDir, collision.gameObject.GetComponent<EnemySword>().owner_En);
            StopAllCoroutines();
            StartCoroutine(Utils.FreezeFrames(swordData.freeze_Intensity, swordData.freeze_Duration));
            StartCoroutine(SwordClash());
            return;
        }
    }

    //Controllare la funzione ed implementarla meglio
    public IEnumerator SwordClash()
    {
        targetDir = initialDir;
        targetRot = Quaternion.LookRotation(targetDir);

        yield return rot_Pivot.DOLocalRotateQuaternion(targetRot, 0.2f).SetEase(swordData.swingEase).WaitForCompletion();

        canRotate = true;
        player_Movement.IsAttacking = false;
        player_Movement.CanMove = true;
        player_Movement.CanDash = true;
        InputController.instance.LeftMouseDown += LR_Swing;
        InputController.instance.RightMouseDown += RL_Swing;
        swinging = false;
    }
}
