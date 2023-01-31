using UnityEngine;

public class InputController : Singleton<InputController>
{
    public delegate void OnLeftMouseDown();
    public event OnLeftMouseDown LeftMouseDown;

    #region AnalogueVariables
    //Parameters for analogs
    private float leftHorizontalInput;
        private float leftVerticalInput;

        private Vector2 leftAnalogDir;

        private float rightHorizontalInput;
        private float rightVerticalInput;

        private Vector2 rightAnalogDir;

    //Parameter for mouse
        private float mouseX;
        private float mouseY;

    public bool usingMouse = false;
    #endregion

    /// <summary>
    /// Getter for all the usefull input value
    /// </summary>
    #region Getter

    public Vector2 LeftStickDir
    {
        get { return leftAnalogDir; }
    }
    
    public Vector2 RightStickDir
    {
        get { return rightAnalogDir; }
    }

    public float RightHorizontalInput
    {
        get { return rightHorizontalInput; }
    }
    
    public float RightVerticalInput
    {
        get { return rightVerticalInput; }
    }

    #endregion
    private void Update()
    {
        LeftAnalogUpdate();
        RightAnalogUpdate();
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseDown();
        }
    }

    private void LeftAnalogUpdate()
    {
        leftHorizontalInput = Input.GetAxis("Horizontal");
        leftVerticalInput = Input.GetAxis("Vertical");

        leftAnalogDir = new Vector2(leftHorizontalInput, leftVerticalInput);
    }
    
    private void RightAnalogUpdate()
    {
        rightHorizontalInput = Input.GetAxis("RightAnalogX");
        rightVerticalInput = Input.GetAxis("RightAnalogY");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Prioritizing the joypad controller then check the board for right analog input
        if (rightVerticalInput != 0 || rightHorizontalInput != 0)
        {
            rightAnalogDir = new Vector2(rightHorizontalInput, rightVerticalInput);
            usingMouse = false;
        }
        else if (mouseX != 0 || mouseY != 0)
        {
            rightAnalogDir = new Vector2(mouseX, mouseY);
            usingMouse = true;
        }
    }    
}
