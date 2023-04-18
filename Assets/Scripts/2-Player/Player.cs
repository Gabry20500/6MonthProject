using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] Player_Data_SO player_SO;
    [SerializeField] PlayerData player_Data;

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

    protected override void Death()
    {
        Debug.Log("Morendo");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
