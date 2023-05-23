using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone_UI : MonoBehaviour
{
    [SerializeField] private StoneElement element = StoneElement.NONE;
    private Image stoneImage;
    private Sprite oldSprite;
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
        oldSprite = stoneImage.sprite;
        stoneImage.sprite = stone.Image;
        isPickedUp = true;
    }
    public void OnStone_Discarded(Stone stone)
    {
        stoneImage.sprite = oldSprite;
        isPickedUp = false;
    }
    public void Activate() 
    {
        isActivated = true;
        stoneImage.rectTransform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
    }
    public void Disable()
    {
        isActivated = false;
        stoneImage.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

}
