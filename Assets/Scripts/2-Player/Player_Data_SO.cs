using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Player_Data_SO", fileName = "New Player_SO")]
public class Player_Data_SO : ScriptableObject
{
    [Header("Entity parameters:")]
    [SerializeField] public float HP;
    [SerializeField] public float max_HP;
    [SerializeField] public AudioClip base_Hit_Clip;
    [Space]
    [Header("EMovement paramenters:")]
    [SerializeField] public float move_Max_Speed;
    [SerializeField] public float acceleration;
    [SerializeField] public float deceleration;
    [Header("Dash paramenters:")]
    [SerializeField] public float dash_Speed;
    [SerializeField] public float dash_Time;
    [SerializeField] public float dash_Cooldown;
    [SerializeField] public Ease dash_Ease;
    [Header("Knock paramenters:")]
    [SerializeField] public Ease knock_Ease;
    [Header("Clash paramenters:")]
    [SerializeField] public Ease clash_Ease;
    [Header("Invincibility paramenters:")]
    [SerializeField] public float inv_Time;
    [SerializeField] public float inv_Flash_Tick;
    [SerializeField] public Color inv_Color = Color.clear;

    public float DashDistance { get => dash_Speed * dash_Time; }

    public void SetData(PlayerData data)
    {
        HP = data.HP;
        max_HP = data.max_HP;
        base_Hit_Clip = data.base_Hit_Clip;
        move_Max_Speed = data.move_Max_Speed;
        acceleration = data.acceleration;
        deceleration = data.deceleration;
        dash_Speed = data.dash_Speed;
        dash_Time = data.dash_Time;
        dash_Cooldown = data.dash_Cooldown;
        dash_Ease = data.dash_Ease;
        knock_Ease = data.knock_Ease;
        clash_Ease = data.clash_Ease;
        inv_Time = data.inv_Time;
        inv_Flash_Tick = data.inv_Flash_Tick;
        inv_Color = data.inv_Color;
    }
}

[System.Serializable]
public class PlayerData
{
    [Header("Entity parameters:")]
    [SerializeField] public float HP;
    [SerializeField] public float max_HP;
    [SerializeField] public AudioClip base_Hit_Clip;
    [Space]
    [Header("EMovement paramenters:")]
    [SerializeField] public float move_Max_Speed;
    [SerializeField] public float acceleration;
    [SerializeField] public float deceleration;
    [Header("Dash paramenters:")]
    [SerializeField] public float dash_Speed;
    [SerializeField] public float dash_Time;
    [SerializeField] public float dash_Cooldown;
    [SerializeField] public Ease dash_Ease;
    [Header("Knock paramenters:")]
    [SerializeField] public Ease knock_Ease;
    [Header("Clash paramenters:")]
    [SerializeField] public Ease clash_Ease;
    [Header("Invincibility paramenters:")]
    [SerializeField] public float inv_Time;
    [SerializeField] public float inv_Flash_Tick;
    [SerializeField] public Color inv_Color;

    public float DashDistance { get => dash_Speed * dash_Time; }

    public PlayerData(Player_Data_SO player_SO)
    {
        HP = player_SO.HP;
        max_HP = player_SO.max_HP;
        base_Hit_Clip = player_SO.base_Hit_Clip;
        move_Max_Speed = player_SO.move_Max_Speed;
        acceleration = player_SO.acceleration;
        deceleration = player_SO.deceleration;
        dash_Speed = player_SO.dash_Speed;
        dash_Time = player_SO.dash_Time;
        dash_Cooldown = player_SO.dash_Cooldown;
        dash_Ease = player_SO.dash_Ease;
        knock_Ease = player_SO.knock_Ease;
        clash_Ease = player_SO.clash_Ease;
        inv_Time = player_SO.inv_Time;
        inv_Flash_Tick = player_SO.inv_Flash_Tick;
        inv_Color = player_SO.inv_Color;
    }

    public void UpdateData(Player player, EMovement player_Mov_Script)
    {
        HP = player.Healt;
        max_HP = player.MaxHealth;
        base_Hit_Clip = player.Hit_Clip;
        move_Max_Speed = player_Mov_Script.Move_Speed;
        acceleration = player_Mov_Script.Acceleration;
        deceleration = player_Mov_Script.Deceleration;
        dash_Speed = player_Mov_Script.Dash_Speed;
        dash_Time = player_Mov_Script.Dash_Time;
        dash_Cooldown = player_Mov_Script.Dash_Cooldown;
        inv_Time = player_Mov_Script.Inv_Time;
        inv_Flash_Tick = player_Mov_Script.Inv_Flash_Tick;
        inv_Color = player_Mov_Script.Inv_Color;
    }
}
