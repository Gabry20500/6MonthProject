using UnityEngine.UI;
using UnityEngine;

public class Mana_UI : MonoBehaviour
{
    [SerializeField] private Sprite disabled_Sprite;
    [SerializeField] private Sprite enabled_Sprite;
    [SerializeField] private bool isPossessed = false;
    private Sprite UI_Sprite;

    public bool IsPossessed
    {
        get { return isPossessed; }
    }

    private void Awake()
    {
        UI_Sprite = GetComponent<Image>().sprite;
    }
    public void Activate()
    {
        isPossessed = true;
        UI_Sprite = enabled_Sprite;

    }

    public void Disable()
    {
        isPossessed = false;
        UI_Sprite = disabled_Sprite;
    }
}
