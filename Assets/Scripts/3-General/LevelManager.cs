using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

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
