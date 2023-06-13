using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Stone")] public int randValue;
    [SerializeField] private List<GameObject> usableStone;
    [SerializeField] private List<Stone> ownedStone;
    
    [Header("Progression")]
    public int level = -1;


    #region Getter

    public List<GameObject> UsableStone
    {
        get { return usableStone; }
    }
    
    public List<Stone>OwnedStone
    {
        get { return ownedStone; }
    }

    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    

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


    public void RemoveStone(GameObject stone)
    {
        Stone stoneObj = stone.GetComponent<Stone_Object>().Stone;
        OwnedStone.Add(stoneObj);
        foreach (GameObject UsableStone in usableStone)
        {
            if (stoneObj.Element ==
                UsableStone.GetComponent<Stone_Object>().Stone.Element)
            {
                usableStone.Remove(UsableStone);
                return;
            }
        }
        
        
    }
}
