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
        if (InputController.instance.RightStickDir != Vector2.zero)
        {
            float rotation = Vector3.Angle(InputController.instance.RightStickDir,transform.forward);
            if (currentDir != InputController.instance.RightStickDir)
            {
                currentDir = InputController.instance.RightStickDir;
                transform.RotateAround(target.position, Vector3.up, rotation);
            }
        }
    }
}
