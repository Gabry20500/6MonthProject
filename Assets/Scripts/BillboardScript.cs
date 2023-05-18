using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    Camera mainCamera;
    Quaternion objRotation;
    
    void Start()
    {
        mainCamera = Camera.main;
        objRotation = transform.rotation;
        
        Quaternion newRotation = new Quaternion(mainCamera.transform.rotation.x, transform.rotation.y,
            mainCamera.transform.rotation.z, mainCamera.transform.rotation.w);
        transform.rotation = newRotation;
    }
    
}
