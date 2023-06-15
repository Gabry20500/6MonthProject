using UnityEngine.UI;
using UnityEngine;

public class Mana_UI : MonoBehaviour
{
    [SerializeField] private Sprite disabled_Sprite;
    [SerializeField] private Sprite enabled_Sprite;
    [SerializeField] private bool isPossessed = false;
    private Image UI_Image;

    public bool IsPossessed
    {
        get { return isPossessed; }
    }

    private void Awake()
    {
        UI_Image = GetComponent<Image>();
    }
    public void Activate()
    {
        isPossessed = true;
        UI_Image.sprite = enabled_Sprite;

    }

    public void Disable()
    {
        isPossessed = false;
        UI_Image.sprite = disabled_Sprite;
    }
}
