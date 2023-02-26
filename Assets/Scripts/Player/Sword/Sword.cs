using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// SwordScript class that manage an item to act like a sword around his Y axis
/// </summary>
public class Sword : MonoBehaviour
{
    [SerializeField] private SwordDataSO swordData;
    [SerializeField] public SwordData sword;

    [SerializeField] public bool canRotate = true;

    #region MouseTracing
    private Vector2 mousePos;
    private Ray ray; //Ray to detect the world poin hitted by the mouse position
    private RaycastHit hit;
    private Vector3 currentDir; //Real world 3D direction elaborated from the mouse-world hit position
    #endregion

    private EMovement _entity; //Link to the entity movement class to sto his movement and to perform other actions
    [SerializeField] private EAnimator animator; //Animation class of the entity to link in editor to call the swing animation on the sprite
    [SerializeField] Transform pivot;
    
    #region SwingAnimation parameters
    private Vector3 initialDir;
    private Vector3 targetDir;
    Quaternion targetRot;
    #endregion
    [SerializeField] AudioClip baseSwingEffect;
    [SerializeField] AudioClip baseClashEffect;
    [SerializeField] AudioSource audioSource;

    public float Damage
    {
        get
        {
            return sword.Damage;
        }
    }

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.clip = baseSwingEffect;
        _entity = gameObject.GetComponentInParent<EMovement>();
        sword = new SwordData(swordData);
    }

    /// <summary>
    /// Inscribe Swing function to desired event in InputController
    /// </summary>
    private void OnEnable()
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
                currentDir = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
                pivot.forward = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
            }
            //More elaborations needed to obtain direction from mouse pointing the world
            else if (InputController.instance.usingMouse)
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Vector2 of the mouse position in the screen
                ray = Camera.main.ScreenPointToRay(mousePos);//Generate a ray from the mouse pos in the direction of the world from the camera
                Physics.Raycast(ray, out hit);//Detect collision
                currentDir = (hit.point - pivot.position).normalized; //Calculate direction from the entity and the hitted world point
                currentDir = new Vector3(currentDir.x, 0.0f, currentDir.z); //Generate a new 3D vector
                pivot.forward = currentDir;//Set sword direction
            }
        }
    }

    /// <summary>
    /// Swing public function to perform a swing
    /// </summary>
    public void Swing()
    {
        Debug.Log("Swinging");
        _entity.CanMove = false; //The linked entity can't move
        canRotate = false; //The sword can no more rotate
        //entityAnimation.SetDirection(new Vector2(currentDir.x, currentDir.z)); //Put the player in the swing direction before everything else
        animator.AttackAnimation(new Vector2(currentDir.x, currentDir.z));//Call the appropriate attack animation
        InputController.instance.LeftMouseDown -= Swing; //Unscribe from Input controller to avoid spam
        //Starting the coroutine swing animation
        StartCoroutine(SwingAnimation(currentDir));
    }

    int i = 1;
    private IEnumerator SwingAnimation(Vector3 swingDir)
    {

         initialDir = Quaternion.AngleAxis(-(sword.swingWidth / 2) * i, Vector3.up) * pivot.forward; //Calculcate the initial direction of the swing animation
         pivot.forward = initialDir;//Set forward to the initial position of the animation
         targetDir = Quaternion.AngleAxis(+(sword.swingWidth * 1.2f) * i, Vector3.up) * pivot.forward;//Calculcate the desired final direction 
         Quaternion targetRot = Quaternion.LookRotation(targetDir);

        i *= -1;

        float t = 0.0f; //Timer
        float percentace = 0.0f;//Percentage for lerp

        //Call the attack dash function in the entity attached to this script
       // _entity.StartCoroutine(AtkDash(new Vector2(swingDir.x, swingDir.z), sword.dashSpeed, sword.dashDuration));

        audioSource.Play();
        while (t < sword.swingSpeed) //Cicle
        {
            percentace = t / sword.swingSpeed;//New percentage
            Quaternion nextRotation = Quaternion.Lerp(pivot.localRotation, targetRot, percentace);//Calculate next rotation stem
            pivot.localRotation = nextRotation; //Change rotation

            yield return null;
            t += Time.deltaTime;//Increment timer         
        }

        pivot.forward = targetDir;//Safe repositioning??

        yield return new WaitForSeconds(sword.swingCoolDown);
        animator.SetDirection(new Vector2(currentDir.x, currentDir.z));//Set player animation to the resting actual direction
        canRotate = true;//Enable sword movement
        _entity.CanMove = true;//Enable entity movment
        InputController.instance.LeftMouseDown += Swing;//Re-inscribe swing to InputController
    }

    public IEnumerator AtkDash(Vector2 direction, float dashAtkSpeed, float duration)
    {
        _entity.CanMove = false;
        _entity.IsDashing = false;
        _entity.CanDash = false;
        _entity.IsAttacking = true;

        float t = 0.0f; //Timer
        while (t < duration) //Cicle
        {
            //Lock the velocity on the dashDirection multyplied for speed * dashingSpeed multyplier
            Vector3 velocity = new Vector3(direction.normalized.x * (_entity.speed * dashAtkSpeed) * Time.fixedDeltaTime, 0,
                                   direction.normalized.y * (_entity.speed * dashAtkSpeed) * Time.fixedDeltaTime);

            _entity.Rigidbody.velocity = velocity;
            yield return null;
            t += Time.fixedDeltaTime;//Increment timer         
        }

        _entity.IsAttacking = false;
        _entity.CanMove = true;
        _entity.CanDash = true;
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("EnemySword") && canRotate == false && collision.gameObject.GetComponentInParent<EnemyAI>().animator.GetBool("Attack") == true)
        {
            //Passing knock back direction to applicate to te hitted entity
            Vector3 knockBackDir = transform.parent.position - collision.gameObject.transform.position;
            knockBackDir = new Vector3(knockBackDir.x, 0.0f, knockBackDir.z);
            knockBackDir.Normalize();

            this.gameObject.GetComponentInParent<EMovement>().OnClash(knockBackDir, collision.gameObject.GetComponent<EnemySword>().enemyData);
        }

        else if (collision.gameObject.CompareTag("Enemy") && _entity.IsAttacking == true)
        {
            Vector3 knockBackDir = collision.gameObject.transform.position - transform.parent.position;
            knockBackDir = new Vector3(knockBackDir.x, 0.0f, knockBackDir.z);
            knockBackDir.Normalize();

            collision.gameObject.GetComponent<EnemyAI>().OnHit(Damage, knockBackDir, sword);
        }
    }
}
