using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public List<GameObject> disabled;
    public new List<GameObject> enabled;
    
    
    public void Play()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        foreach (var eGameObject in enabled)
        {
            eGameObject.SetActive(false);
        }

        foreach (var dGameObject in disabled)
        {
            dGameObject.SetActive(true);
        }
    }
    
    public void GoBack()
    {
        foreach (var eGameObject in enabled)
        {
            eGameObject.SetActive(true);
        }

        foreach (var dGameObject in disabled)
        {
            dGameObject.SetActive(false);
        }
    }
}
