using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// SwordScript class that manage an item to act like a sword around his Y axis
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private SwordDataSO swordSO;
    [SerializeField] public SwordData swordData;

    [SerializeField] public bool canRotate = true;
    [SerializeField] bool swinging = false;

    #region MouseTracing
    private Vector2 mousePos;
    private Ray ray; //Ray to detect the world poin hitted by the mouse position
    private RaycastHit hit;
    private Vector3 currentDir; //Real world 3D direction elaborated from the mouse-world hit position
    #endregion

    private EMovement e_Movement; //Link to the entity movement class to sto his movement and to perform other actions
    [SerializeField] private EAnimator e_Animator; //Animation class of the entity to link in editor to call the swing animation on the sprite
    [SerializeField] Transform rot_Pivot;

    #region SwingAnimation parameters
    private Vector3 initialDir;
    private Vector3 targetDir;
    Quaternion targetRot;
    #endregion
    [SerializeField] AudioClip baseSwing;
    [SerializeField] AudioClip baseClash;
    [SerializeField] AudioSource sword_Audio;

    public float Damage
    {
        get
        {
            return swordData.Damage;
        }
    }

    private void Awake()
    {
        sword_Audio = GetComponentInParent<AudioSource>();
        sword_Audio.clip = baseSwing;
        e_Movement = gameObject.GetComponentInParent<EMovement>();
        swordData = new SwordData(swordSO);
    }

    /// <summary>
    /// Inscribe Swing function to desired event in InputController
    /// </summary>
    private void Start()
    {
        InputController.instance.LeftMouseDown += Swing;
    }
    /// <summary>
    /// Unscribe Swing function to desired event in InputController
    /// </summary>
    private void OnDisable()
    {
        if (InputController.instance != null)
        {
            InputController.instance.LeftMouseDown -= Swing;
        }
    }
    void Update()
    {
        //Utilities to draw on editor the reach of the sword
        //Debug.DrawLine(pivot.position, transform.forward * 10, Color.green);
        //Vector3 rot = Quaternion.AngleAxis(-(swingWidth/2), Vector3.up) * pivot.forward;
        //Debug.DrawLine(pivot.position, rot * 10, Color.red);
        //rot = Quaternion.AngleAxis((swingWidth * 1.2f), Vector3.up) * pivot.forward;
        //Debug.DrawLine(pivot.position, rot * 10, Color.red);

        if (canRotate)
        {
            //Rotate directly using the axis value from the right stick in gameoad
            if (InputController.instance.usingMouse == false)
            {
                currentDir = new Vector3(InputController.instance.RT_Stick_Dir.x, 0.0f, InputController.instance.RT_Stick_Dir.y);
                rot_Pivot.forward = new Vector3(InputController.instance.RT_Stick_Dir.x, 0.0f, InputController.instance.RT_Stick_Dir.y);
            }
            //More elaborations needed to obtain direction from mouse pointing the world
            else if (InputController.instance.usingMouse)
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Vector2 of the mouse position in the screen
                ray = Camera.main.ScreenPointToRay(mousePos);//Generate a ray from the mouse pos in the direction of the world from the camera
                Physics.Raycast(ray, out hit);//Detect collision
                currentDir = (hit.point - rot_Pivot.position).normalized; //Calculate direction from the entity and the hitted world point
                currentDir = new Vector3(currentDir.x, 0.0f, currentDir.z); //Generate a new 3D vector
                rot_Pivot.forward = currentDir;//Set sword direction
            }
        }
    }

    /// <summary>
    /// Swing public function to perform a swing
    /// </summary>

    public void Swing()
    {
        if (swinging == false)
        {
            swinging = true;
            InputController.instance.LeftMouseDown -= Swing; //Unscribe from Input controller to avoid spam
            e_Movement.CanMove = false; //The linked entity can't move
            canRotate = false; //The sword can no more rotate
            e_Animator.SetDirection(new Vector2(currentDir.x, currentDir.z)); //Put the player in the swing direction before everything else
            //Starting the coroutine swing animation
            StartCoroutine(SwingAnimation(currentDir));
        }
        else if(swinging == true)
        {
            return;
        }
    }

    int i = 1;
    private IEnumerator SwingAnimation(Vector3 swingDir)
    {
         initialDir = Quaternion.AngleAxis(-(swordData.swingWidth / 2) * i, Vector3.up) * rot_Pivot.forward; //Calculcate the initial direction of the swing animation
         rot_Pivot.forward = initialDir;//Set forward to the initial position of the animation
         targetDir = Quaternion.AngleAxis((swordData.swingWidth * 1.2f) * i, Vector3.up) * rot_Pivot.forward;//Calculcate the desired final direction 
         Quaternion targetRot = Quaternion.LookRotation(targetDir);

        i *= -1;

        float t = 0.0f; //Timer
        float percentace = 0.0f;//Percentage for lerp

        //Call the attack dash function in the entity attached to this script
        StartCoroutine(AtkDash(new Vector2(swingDir.x, swingDir.z), swordData.dashSpeed, swordData.dashDuration));

        sword_Audio.Play();
        while (t < swordData.swingSpeed) //Cicle
        {
            percentace = t / swordData.swingSpeed;//New percentage

            Quaternion nextRotation = Quaternion.Lerp(rot_Pivot.localRotation, targetRot, percentace);//Calculate next rotation stem
            rot_Pivot.localRotation = nextRotation; //Change rotation

            yield return null;
            t += Time.deltaTime;//Increment timer         
        }


        rot_Pivot.forward = targetDir;//Safe repositioning??
        yield return new WaitForSeconds(swordData.swingCoolDown);
        canRotate = true;//Enable sword movement
        e_Movement.CanMove = true;//Enable entity movment
        InputController.instance.LeftMouseDown += Swing;//Re-inscribe swing to InputController
        swinging = false;
    }

    public IEnumerator AtkDash(Vector2 direction, float dashAtkSpeed, float duration)
    {
        e_Movement.CanMove = false;
        e_Movement.IsDashing = false;
        e_Movement.CanDash = false;
        e_Movement.IsAttacking = true;

        float t = 0.0f; //Timer
        while (t < duration) //Cicle
        {
            //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
            Vector3 velocity = new Vector3(direction.normalized.x * (e_Movement.speed * dashAtkSpeed) * Time.fixedDeltaTime, 0,
                                   direction.normalized.y * (e_Movement.speed * dashAtkSpeed) * Time.fixedDeltaTime);

            e_Movement.Rigidbody.velocity = velocity;
            yield return null;
            t += Time.fixedDeltaTime;//Increment timer         
        }

        e_Movement.IsAttacking = false;
        e_Movement.CanMove = true;
        e_Movement.CanDash = true;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && e_Movement.IsAttacking == true)
        {
            StartCoroutine(Utilities.FreezeFrames(0.3f, 0.4f));
            Vector3 knockDir = CalculateDir(collision.gameObject.transform.position, transform.parent.position);
            collision.gameObject.GetComponent<EnemyAI>().OnHit(Damage, knockDir, swordData);
            return;
        }
        if (collision.gameObject.CompareTag("EnemySword") && canRotate == false && collision.gameObject.GetComponentInParent<EnemyAI>().animator.GetBool("Attack") == true)
        {
            //Passing knock back direction to applicate to te hitted entity
            Vector3 knockDir = CalculateDir(transform.parent.position, collision.gameObject.transform.position);

            gameObject.GetComponentInParent<EMovement>().OnHit(knockDir, collision.gameObject.GetComponent<EnemySword>().ownerEnemy);
            StopAllCoroutines();
            StartCoroutine(Utilities.FreezeFrames(0.3f, 0.4f));
            StartCoroutine(SwordClash());
            return;
        }
    }

    public IEnumerator SwordClash()
    {
        targetDir = initialDir;
        Quaternion targetRot = Quaternion.LookRotation(targetDir);

        float t = 0.0f;
        float percentace = 0.0f;

        while (t < 0.2f)
        {
            percentace = t / 0.2f;
            Quaternion nextRotation = Quaternion.Lerp(rot_Pivot.localRotation, targetRot, percentace);
            rot_Pivot.localRotation = nextRotation; 

            yield return null;
            t += Time.deltaTime;        
        }
        rot_Pivot.forward = targetDir;
        canRotate = true;
        e_Movement.IsAttacking = false;
        e_Movement.CanMove = true;
        e_Movement.CanDash = true;
        InputController.instance.LeftMouseDown += Swing;
        swinging = false;
    }


    private Vector3 CalculateDir(Vector3 A, Vector3 B)
    {
        Vector3 dir = A - B;
        dir = new Vector3(dir.x, 0.0f, dir.z);
        return dir.normalized;
    }
}
