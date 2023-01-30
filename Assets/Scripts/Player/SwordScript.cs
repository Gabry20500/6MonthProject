using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] GameObject obj;
    void Update()
    {
        //transform.forward = new Vector3(InputController.instance.RightStickDir.x, 0.0f, InputController.instance.RightStickDir.y);
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Instantiate(obj, hit.point, Quaternion.identity);
        Vector3 newDir = (hit.point - transform.position).normalized;
        newDir = new Vector3(newDir.x, 0.0f, newDir.z);
        transform.forward = newDir;
    }
}
