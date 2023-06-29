using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuseMenuButton : MonoBehaviour, ISubmitHandler
{
    [SerializeField] private bool isSelected = false;
    [SerializeField] private Button thisButton;
    [SerializeField] private List<Button> buttons;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            thisButton.interactable = false;
            thisButton.interactable = true; 
            buttons[1].Select();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            thisButton.interactable = false;
            thisButton.interactable = true; 
            buttons[0].Select();
        }
            

    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }
}
