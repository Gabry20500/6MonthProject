using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] Player_Data_SO player_SO;
    [SerializeField] PlayerData player_Data;

    protected override void Awake()
    {
        base.Awake();
        player_Data = new PlayerData(player_SO);
        InitParameters();
        GetComponent<EMovement>().InitParameters(player_Data);
    }

    private void InitParameters()
    {
        HP = player_Data.HP;
        max_HP = player_Data.max_HP;
        hit_Clip = player_Data.base_Hit_Clip;
    }
}
