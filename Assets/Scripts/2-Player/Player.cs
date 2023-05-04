using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] Player_Data_SO player_SO;
    [SerializeField] PlayerData player_Data;
    [SerializeField] private List<Sprite> healthBar_playerIcon;
    [SerializeField] private Image currentImage;

    protected override void Awake()
    {
        base.Awake();
        player_Data = new PlayerData(player_SO);
        InitParameters();  
    }
    
    

    private void InitParameters()
    {
        HP = player_Data.HP;
        max_HP = player_Data.max_HP;
        InitHealthBar();
        hit_Clip = player_Data.base_Hit_Clip;
        GetComponent<EMovement>().self = player_Data;
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);
        if (HP/max_HP <= 0.5f)
        {
            currentImage.sprite = healthBar_playerIcon[1];
        }
        else
        {
            currentImage.sprite = healthBar_playerIcon[0];
        }
    }

    protected override void Death()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
