using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    [Header("Stone")] public int randValue;
    [SerializeField] private List<GameObject> usableStone;
    private List<GameObject> stoneList;
    [SerializeField] private List<Stone> ownedStone;
    
    [Header("Progression")]
    public int level = -1;
    public bool isTutorialComplete;


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

    private void Awake()
    {
        stoneList = new List<GameObject>(usableStone);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            level = -1;
            ResetStone();
        }
    }

    public void ResetStone()
    {
        ownedStone = new List<Stone>();
        usableStone = new List<GameObject>(stoneList);
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


    public void AcquireStone(GameObject stone)
    {
        Stone stoneObj = stone.GetComponent<Stone_Object>().Stone;
        
        if (level > 0 && IsStoneAcquired(stoneObj) == false)
        {
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

    private bool IsStoneAcquired(Stone stone)
    {
        foreach (Stone _ownedStone in ownedStone)
        {
            if (stone.Element == _ownedStone.Element)
            {
                return true;
            }
        }

        return false;
    }
}
