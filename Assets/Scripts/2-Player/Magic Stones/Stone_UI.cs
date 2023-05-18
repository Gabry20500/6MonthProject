using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone_UI : MonoBehaviour
{
    [SerializeField] private StoneElement element = StoneElement.NONE;
    private Image stoneImage;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private bool isPickedUp = false;

    #region Getter
    public StoneElement Element
    {  
        get { return element; } 
    }
    public Image StoneImage
    {
        get { return stoneImage; }
    }
    public bool IsActivated
    {
        get { return isActivated; }
    }
    public bool IsPickedUp
    {
        get { return isPickedUp; }
    }
    #endregion

    private void Awake()
    {
        stoneImage = GetComponent<Image>();
    }

    public void OnStone_PickedUp(Stone stone) 
    { 

    }
    public void Activate() 
    { 
    
    }
    public void Disable()
    {
        
    }

}
