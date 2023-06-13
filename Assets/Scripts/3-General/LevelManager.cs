using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public int level = -1;

    public void increaseLevel()
    {
        level++;
    }
    
    public float IncrementFloatStats(float value)
    {
        float percentage = 0.15f; // Increment percentage (15%)
        float incrementAmount = value * percentage; // Increment amount
        float finalIncrement = incrementAmount * level; // Final increment based on the index

        float newValue = value + finalIncrement; // New float value after the increment

        return newValue;
    }
}
