using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Player_SwordData
{
    public string name;
    [Header("Swing:")]
    public Ease swingEase;
    public float swingSpeed;
    public float swingWidth;
    public float swingCoolDown;
    [Header("Damage:")]
    public float physicDamage;
    [Header("KnockBack:")]
    public float knockSpeed;
    public float knockDuration;
    [Header("Attack dash:")]
    public float dashSpeed;
    public float dashDuration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;

    public float DashDistance { get => dashSpeed * dashDuration; }
    public float Damage { get => physicDamage; }

    public Player_SwordData() { }
    public Player_SwordData(Player_SwordSO swordSO)
    {
        this.name = swordSO.name;
        this.swingEase = swordSO.swingEase;
        this.swingSpeed = swordSO.swingSpeed;       
        this.swingWidth = swordSO.swingWidth;
        this.swingCoolDown = swordSO.swingCoolDown;
        this.physicDamage = swordSO.physicDamage;
        this.knockSpeed = swordSO.knockSpeed;
        this.knockDuration = swordSO.knockDuration;
        this.dashSpeed = swordSO.dashSpeed;
        this.dashDuration = swordSO.dashDuration;
        this.swordScale = swordSO.swordScale;
        this.isBlockable = swordSO.isBlockable;
    }
}

[CreateAssetMenu(menuName = "Player_SwordSO", fileName = "New Player_SwordSO")]
public class Player_SwordSO : ScriptableObject
{
    [Header("Swing:")]
    public Ease swingEase; 
    public float swingSpeed;
    public float swingWidth;
    public float swingCoolDown;
    [Header("Damage:")]
    public float physicDamage;
    [Header("KnockBack:")]
    public float knockSpeed;
    public float knockDuration;
    [Header("Attack dash:")]
    public float dashSpeed;
    public float dashDuration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;
    public float DashDistance { get => dashSpeed * dashDuration; }

    public void SetData(Player_SwordData data)
    {
        this.name = data.name;
        this.swingEase = data.swingEase;
        this.swingSpeed = data.swingSpeed;
        this.swingWidth = data.swingWidth;
        this.swingCoolDown = data.swingCoolDown;
        this.physicDamage = data.physicDamage;
        this.knockSpeed = data.knockSpeed;
        this.knockDuration = data.knockDuration;
        this.dashSpeed = data.dashSpeed;
        this.dashDuration = data.dashDuration;
        this.swordScale = data.swordScale;
        this.isBlockable = data.isBlockable;
    }
}
