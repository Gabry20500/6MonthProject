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
    [Header("Freeeze frames:")]
    public float freeze_Intensity;
    public float freeze_Duration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;

    public float DashDistance { get => dashSpeed * dashDuration; }
    public float Damage { get => physicDamage; }

    public Player_SwordData() { }
    public Player_SwordData(Player_SwordSO swordSO)
    {
        name = swordSO.name;
        swingEase = swordSO.swingEase;
        swingSpeed = swordSO.swingSpeed;       
        swingWidth = swordSO.swingWidth;
        swingCoolDown = swordSO.swingCoolDown;
        physicDamage = swordSO.physicDamage;
        knockSpeed = swordSO.knockSpeed;
        knockDuration = swordSO.knockDuration;
        dashSpeed = swordSO.dashSpeed;
        dashDuration = swordSO.dashDuration;
        freeze_Intensity = swordSO.freeze_Intensity;
        freeze_Duration = swordSO.freeze_Duration;
        swordScale = swordSO.swordScale;
        isBlockable = swordSO.isBlockable;
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
    [Header("Freeeze frames:")]
    public float freeze_Intensity;
    public float freeze_Duration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;
    public float DashDistance { get => dashSpeed * dashDuration; }

    public void SetData(Player_SwordData data)
    {
        name = data.name;
        swingEase = data.swingEase;
        swingSpeed = data.swingSpeed;
        swingWidth = data.swingWidth;
        swingCoolDown = data.swingCoolDown;
        physicDamage = data.physicDamage;
        knockSpeed = data.knockSpeed;
        knockDuration = data.knockDuration;
        dashSpeed = data.dashSpeed;
        dashDuration = data.dashDuration;
        freeze_Intensity = data.freeze_Intensity;
        freeze_Duration = data.freeze_Duration;
        swordScale = data.swordScale;
        isBlockable = data.isBlockable;
    }
}
