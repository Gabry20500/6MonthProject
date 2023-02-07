using System.Collections;
using UnityEngine;

/// <summary>
/// SwordScript class that manage an item to act like a sword around his Y axis
/// </summary>
public class SwordScript : MonoBehaviour
{
    [SerializeField] public bool canRotate = true;

    [Header("Swing values")] 
    [SerializeField] float swingWidth = 180.0f; //Range of motion of the swinging sword
    [SerializeField] float swingSpeed = 2f; //The duration of the swing movement
    [SerializeField] float swingCoolDown = 0.1f; //CoolDown to prevent spam
    [SerializeField] float swingForce; //Not yet implemented value

    [Header("DashAttack values")]
    [SerializeField] private float dashSpeed = 4.0f;
    [SerializeField] private float dashDuration = 0.10f;

    #region MouseTracing
    private Vector2 mousePos;
    private Ray ray; //Ray to detect the world poin hitted by the mouse position
    private RaycastHit hit;
    private Vector3 currentDir; //Real world 3D direction elaborated from the mouse-world hit position
    #endregion

    private EntityMovement myEntity; //Link to the entity movement class to sto his movement and to perform other actions
    [SerializeField] private MovementAnimation entityAnimation; //Animation class of the entity to link in editor to call the swing animation on the sprite

    #region SwingAnimation parameters
    //Swing animation parameters
    private Vector3 initialDir;
    private Vector3 targetDir;
    Quaternion targetRot;
    #endregion



    private void Awake()
    {
        myEntity = gameObject.GetComponentInParent<EntityMovement>();
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
        //Debug.DrawLine(transform.position, transform.forward * 10, Color.green);
        //Vector3 rot = Quaternion.AngleAxis(-(swingWidth/2), Vector3.up) * transform.forward;
        //Debug.DrawLine(transform.position, rot * 10, Color.red);
        //rot = Quaternion.AngleAxis((swingWidth / 2), Vector3.up) * transform.forward;
        //Debug.DrawLine(transform.position, rot * 10, Color.red);

        if (canRotate)
        {
            //Rotate directly using the axis value from the right stick in gameoad
            if (InputController.instance.usingMouse == false)
            {
                currentDir = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
                transform.forward = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
            }
            //More elaborations needed to obtain direction from mouse pointing the world
            else if (InputController.instance.usingMouse)
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Vector2 of the mouse position in the screen
                ray = Camera.main.ScreenPointToRay(mousePos);//Generate a ray from the mouse pos in the direction of the world from the camera
                Physics.Raycast(ray, out hit);//Detect collision
                currentDir = (hit.point - transform.position).normalized; //Calculate direction from the entity and the hitted world point
                currentDir = new Vector3(currentDir.x, 0.0f, currentDir.z); //Generate a new 3D vector
                transform.forward = currentDir;//Set sword direction
            }
        }
    }

    /// <summary>
    /// Swing public function to perform a swing
    /// </summary>
    public void Swing()
    {
        myEntity.canMove = false; //The linked entity can't move
        canRotate = false; //The sword can no more rotate
        //entityAnimation.SetDirection(new Vector2(currentDir.x, currentDir.z)); //Put the player in the swing direction before everything else
        entityAnimation.AttackAnimation(new Vector2(currentDir.x, currentDir.z));//Call the appropriate attack animation
        InputController.instance.LeftMouseDown -= Swing; //Unscribe from Input controller to avoid spam
        //Starting the coroutine swing animation
        StartCoroutine(SwingAnimation(currentDir));
    }

    private IEnumerator SwingAnimation(Vector3 swingDir)
    {
        initialDir = Quaternion.AngleAxis(-(swingWidth / 2), Vector3.up) * transform.forward; //Calculcate the initial direction of the swing animation
        transform.forward = initialDir;//Set forward to the initial position of the animation
        targetDir = Quaternion.AngleAxis(+(swingWidth), Vector3.up) * transform.forward;//Calculcate the desired final direction 
        Quaternion targetRot = Quaternion.LookRotation(targetDir);


        float t = 0.0f; //Timer
        float percentace = 0.0f;//Percentage for lerp

        //Call the attack dash function in the entity attached to this script
        myEntity.StartCoroutine(myEntity.AttackDash(new Vector2(swingDir.x, swingDir.z), dashSpeed, dashDuration));

        while (t < swingSpeed) //Cicle
        {
            percentace = t / swingSpeed;//New percentage
            Quaternion nextRotation = Quaternion.Lerp(transform.localRotation, targetRot, percentace);//Calculate next rotation stem
            transform.localRotation = nextRotation; //Change rotation

            yield return null;
            t += Time.deltaTime;//Increment timer         
        }

        transform.forward = targetDir;//Safe repositioning??

        yield return new WaitForSeconds(swingCoolDown);
        entityAnimation.SetDirection(new Vector2(currentDir.x, currentDir.z));//Set player animation to the resting actual direction
        canRotate = true;//Enable sword movement
        myEntity.canMove = true;//Enable entity movment
        InputController.instance.LeftMouseDown += Swing;//Re-inscribe swing to InputController
    }
}
