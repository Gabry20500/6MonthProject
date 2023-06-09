using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        LevelManager.instance.increaseLevel();
        LevelManager.instance.increaseLevel();
        SceneManager.LoadScene("LevelGenerationTest");
    }
    
    public void ShowCase()
    {
        SceneManager.LoadScene("Showcase 3d");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
