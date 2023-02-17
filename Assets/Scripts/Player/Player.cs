using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider plySlider;
    private int currentHp = 30;
    private int maxHp = 30;

    private int attack = 8;

    #region Getter

    public int CurrentHp
    {
        get { return currentHp; }
    }
    
    public int MaxHp
    {
        get { return maxHp; }
    }
    
    public int Attack
    {
        get { return attack; }
    }

    #endregion

    private void Awake()
    {
        plySlider.maxValue = maxHp;
        plySlider.value = currentHp;
    }

    public void SetCurrentHp(int damage)
    {
        currentHp -= damage;
        plySlider.value = currentHp;
    }
}
