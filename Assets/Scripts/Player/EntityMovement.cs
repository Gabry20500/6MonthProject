using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    private Vector3 velocity;

    private Rigidbody myRb;
    private MovementAnimation myAnimation;

    public bool canMove = true;
    
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
        InputController.instance.SpaceDown -= StartDash;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            direction = InputController.instance.LeftStickDir;
            velocity =  new Vector3(InputController.instance.LeftStickDir.normalized.x * speed * Time.fixedDeltaTime, 0,
                InputController.instance.LeftStickDir.normalized.y* speed * Time.fixedDeltaTime);
            myRb.velocity = velocity;
            myAnimation.SetDirection(direction);
        }else if (isDashing)
        {
            velocity =  new Vector3(dashDir.normalized.x * (speed * dashingSpeed) * Time.fixedDeltaTime, 0, 
                dashDir.normalized.y * (speed * dashingSpeed) * Time.fixedDeltaTime);

            myRb.velocity = velocity;
        }
    }

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

    public void StartDash()
    {
        if(canDash)
        {
            dashDir = InputController.instance.LeftStickDir;
            StartCoroutine(Dash());
        }
    }




}
