using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    [SerializeField] private Transform target;

    [SerializeField] private float speed;

    private Vector2 currentDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.instance.RightHorizontalInput != 0)
        {
            transform.RotateAround(target.position, Vector3.up, speed * InputController.instance.RightHorizontalInput * Time.deltaTime);
        }else 
        if(InputController.instance.RightVerticalInput != 0) 
            transform.RotateAround(target.position, Vector3.up, speed * InputController.instance.RightVerticalInput * Time.deltaTime);
    }
}
