using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private Vector3 velocity;

    private Rigidbody myRb;
    private Animation myAnimation;

    #region Getter
    public Vector2 Direction
    {
        get { return direction; }
    }
    #endregion

    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myAnimation = GetComponentInChildren<Animation>();
    }


    void FixedUpdate()
    {
        //transform.Translate(InputController.instance.LeftStickDir.normalized * speed * Time.fixedDeltaTime);
        direction = InputController.instance.LeftStickDir;
        velocity = new Vector3(InputController.instance.LeftStickDir.normalized.x * speed * Time.fixedDeltaTime, 0, InputController.instance.LeftStickDir.normalized.y * speed * Time.fixedDeltaTime);
        myRb.velocity = velocity;
        myAnimation.SetDirection(direction);
    }


}
