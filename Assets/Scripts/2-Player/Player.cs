using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private Player_Data_SO player_SO;
    [SerializeField] private PlayerData player_Data;

    [SerializeField] private List<Sprite> health_Icons;
    [SerializeField] private Image currentImage;
    [SerializeField] public Slider dashBar;

    [SerializeField] public List<Stone_UI> stone_UIs;
    [SerializeField] public List<Mana_UI> mana_UIs;

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
            currentImage.sprite = health_Icons[1];
        }
        else
        {
            currentImage.sprite = health_Icons[0];
        }
    }
    protected override void Death()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public IEnumerator Dash_Bar_Cooldown(float coolTime)
    {
        float buff = 0.0f;
        while(buff < coolTime)
        {
            dashBar.value += Time.deltaTime / coolTime;
            buff += Time.deltaTime;
            yield return null;
        }
        dashBar.value = 1.0f;
    }


    // Stone functions with UI
    public void PickUp_Stone(Stone stone)
    {
        foreach(Stone_UI ui in stone_UIs)
        {
            if(ui.Element == stone.Element && ui.IsPickedUp == false)
            {
                ui.OnStone_PickedUp(stone);
            }
        }
    }
    public void Discard_Stone(Stone stone)
    {
        foreach (Stone_UI ui in stone_UIs)
        {
            if (ui.Element == stone.Element && ui.IsPickedUp == true)
            {
                ui.OnStone_Discarded(stone);
            }
        }
    }
    public void Activate_Stone(Stone stone)
    {
        if(stone.Element == StoneElement.NONE) { return; }
        else
        {
            foreach(Stone_UI stone_UI in stone_UIs)
            {
                if(stone_UI.Element == stone.Element)
                { stone_UI.Activate(); }
                else { stone_UI.Disable(); }
            }
        }

    }
    public void Disable_Stone(Stone stone)
    {
        if (stone.Element == StoneElement.NONE) { return; }
        else
        {
            foreach (Stone_UI stone_UI in stone_UIs)
            {
                stone_UI.Disable();
            }
        }
    }

    //Mana functions with UI
    public void Use_Mana()
    {
        foreach(Mana_UI mana in mana_UIs)
        {
            if(mana.IsPossessed == true)
            {
                mana.Disable();
                return;
            }
        }
    }
    public void Add_Mana()
    {
        foreach (Mana_UI mana in mana_UIs)
        {
            if (mana.IsPossessed == false)
            {
                mana.Activate();
                return;
            }
        }
    }
}
