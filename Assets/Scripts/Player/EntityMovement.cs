using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private Vector3 velocity;

    private Rigidbody myRb;
    private MovementAnimation myAnimation;

    public bool canMove = true;

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


    void FixedUpdate()
    {
        if (canMove)
        {
            direction = InputController.instance.LeftStickDir;
            velocity = new Vector3(InputController.instance.LeftStickDir.normalized.x * speed * Time.fixedDeltaTime, 0, InputController.instance.LeftStickDir.normalized.y * speed * Time.fixedDeltaTime);
            myRb.velocity = velocity;
            myAnimation.SetDirection(direction);
        }
    }




}
