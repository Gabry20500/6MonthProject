using UnityEngine;

public class InputController : Singleton<InputController>
{
    #region AnalogueVariables
        private float leftHorizontalInput;
        private float leftVerticalInput;

        private Vector2 leftAnalogDir;

        private float rightHorizontalInput;
        private float rightVerticalInput;

        private Vector2 rightAnalogDir;

        private float mouseX;
        private float mouseY;
    #endregion

    #region Getter

    public Vector2 LeftStickDir
    {
        get { return leftAnalogDir; }
    }
    
    public Vector2 RightStickDir
    {
        get { return rightAnalogDir; }
    }

    #endregion
    private void Update()
    {
        LeftAnalogUpdate();
        RightAnalogUpdate();
    }

    private void LeftAnalogUpdate()
    {
        leftHorizontalInput = Input.GetAxis("Horizontal");
        leftVerticalInput = Input.GetAxis("Vertical");

        leftAnalogDir = new Vector2(leftHorizontalInput, leftVerticalInput);
        //leftAnalogDir.Normalize();
    }
    
    private void RightAnalogUpdate()
    {
        rightHorizontalInput = Input.GetAxis("RightAnalogX");
        rightVerticalInput = Input.GetAxis("RightAnalogY");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (rightVerticalInput != 0 || rightHorizontalInput != 0)
        {
            rightAnalogDir = new Vector2(rightHorizontalInput, rightVerticalInput);
            //rightAnalogDir.Normalize();
        }
        else if (mouseX != 0 || mouseY != 0)
        {
            rightAnalogDir = new Vector2(mouseX, mouseY);
            //rightAnalogDir.Normalize();
        }

    }    
}
