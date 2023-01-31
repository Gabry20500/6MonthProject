using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private Vector2 mousePos;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 newDir;

    public bool canMove = true;
    private EntityMovement entityMov;
    [SerializeField] private MovementAnimation animator;

    private void Awake()
    {
        entityMov = gameObject.GetComponentInParent<EntityMovement>();
    }
    private void OnEnable()
    {
        InputController.instance.LeftMouseDown += Swing;
    }
    private void OnDisable()
    {
        InputController.instance.LeftMouseDown -= Swing;
    }
    void Update()
    {
        if (canMove)
        {
            if (InputController.instance.usingMouse == false)
            {
                transform.forward = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
            }
            else if (InputController.instance.usingMouse)
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                ray = Camera.main.ScreenPointToRay(mousePos);
                Physics.Raycast(ray, out hit);
                newDir = (hit.point - transform.position).normalized;
                newDir = new Vector3(newDir.x, 0.0f, newDir.z);
                transform.forward = newDir;
            }
        }
    }

    public void Swing()
    {
        canMove = false;
        entityMov.canMove = false;
        animator.AttackAnimation(new Vector2(newDir.x, newDir.z));
        StartCoroutine(SwingAnimation());

    }

    private IEnumerator SwingAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        yield return null;
        canMove = true;
        entityMov.canMove = true;
    }
}
