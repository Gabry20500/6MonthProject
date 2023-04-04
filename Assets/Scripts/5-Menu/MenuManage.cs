using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
