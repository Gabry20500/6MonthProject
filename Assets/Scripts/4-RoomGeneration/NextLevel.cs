using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("LevelGenerationTest");
    }
}
