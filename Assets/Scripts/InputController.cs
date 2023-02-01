using UnityEngine;

public class InputController : Singleton<InputController>
{
    /// <summary>
    /// Generic event for Key or Button down.
    /// </summary>
    public delegate void OnButtonDown();
    /// <summary>
    /// Event for Left mouse click on Computer.
    /// </summary>
    public event OnButtonDown LeftMouseDown;
    /// <summary>
    /// Event for Space key down on computer.
    /// </summary>
    public event OnButtonDown SpaceDown;
    

    #region AnalogueVariables
    //Parameters for analogs
    [Header("Stick direction values")]
        private float leftHorizontalInput;
        private float leftVerticalInput;

        [SerializeField] private Vector2 leftAnalogDir;

        private float rightHorizontalInput;
        private float rightVerticalInput;

        [SerializeField] private Vector2 rightAnalogDir;

    //Parameter for mouse
        private float mouseX;
        private float mouseY;

    //Usefull information to detect how elaborate input because on pad the direction is a pure Vector on which direction the elements
    //are ready to work, for the mouse input we must before detect the area indicated by the cursor.
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
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            if(LeftMouseDown != null) 
            { 
                LeftMouseDown(); 
            }          
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (SpaceDown != null)
            {
                SpaceDown();
            }
        }
    }

    /// <summary>
    /// Obtaining axis value from Pad analogues or KeyBoard button defined in ProjectSettings
    /// </summary>
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
        //Otherwise detect mouse screen position and transleta it in a vector
        else if (mouseX != 0 || mouseY != 0)
        {
            rightAnalogDir = new Vector2(mouseX, mouseY);
            usingMouse = true;
        }
    }    
}
