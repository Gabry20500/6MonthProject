using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManage : MonoBehaviour
{
    [SerializeField]private List<GameObject> menuGameObject;
    [SerializeField]private Image backgroundImage;
    [SerializeField]private GameObject goBackButton;
    [SerializeField] private List<Sprite> backImage;
    
    public void StartGame()
    {
        if (LevelManager.instance.isTutorialComplete)
        {
            Time.timeScale = 1;
            LevelManager.instance.increaseLevel();
            LevelManager.instance.increaseLevel();
            SceneManager.LoadScene("LevelGenerationTest");
        }
        else
        {
            Time.timeScale = 1;
            LevelManager.instance.increaseLevel();
            LevelManager.instance.isTutorialComplete = true;
            SceneManager.LoadScene("TutorialLevel"); 
        }
        
    }
    
    public void ShowCredits()
    {
        foreach (GameObject _gameObject in menuGameObject)
        {
            _gameObject.SetActive(false);
            backgroundImage.sprite = backImage[1];
            goBackButton.SetActive(true);
            goBackButton.GetComponent<Button>().interactable = true;
            goBackButton.GetComponent<Button>().Select();
            
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        foreach (GameObject _gameObject in menuGameObject)
        {
            _gameObject.SetActive(true);
            backgroundImage.sprite = backImage[0];
            goBackButton.SetActive(false);
        }

        menuGameObject[3].GetComponent<Button>().interactable = true;
        menuGameObject[3].GetComponent<Button>().Select();
    }
}
