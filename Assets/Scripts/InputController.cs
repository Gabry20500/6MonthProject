using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    private float leftHorizontalInput;
    private float leftVerticalInput;
    private Vector3 leftStickDir;

    
    private float rightHorizontalInput;
    private float rightVerticalInput;
    private Vector3 rightStickDir;

    #region Getter

    public Vector3 LeftStickDir
    {
        get { return leftStickDir; }
    }
    
    public Vector3 RightStickDir
    {
        get { return rightStickDir; }
    }

    #endregion
    private void Update()
    {
        LeftAnalog();
        RightAnalog();
    }

    private void LeftAnalog()
    {
        leftHorizontalInput = Input.GetAxis("Horizontal");
        leftVerticalInput = Input.GetAxis("Vertical");

        leftStickDir = new Vector3(leftHorizontalInput, 0, leftVerticalInput);
        leftStickDir.Normalize();
    }
    
    private void RightAnalog()
    {
        rightHorizontalInput = Input.GetAxis("RightAnalogX");
        rightVerticalInput = Input.GetAxis("RightAnalogY");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (rightVerticalInput != 0 || rightHorizontalInput != 0)
        {
            rightStickDir = new Vector3(rightHorizontalInput, 0, rightVerticalInput);
        }else if (mouseX != 0 || mouseY != 0)
        {
            rightStickDir = new Vector3(mouseX, 0, mouseY);
        }
        rightStickDir.Normalize();
    }

    
}
