using UnityEngine;

[System.Serializable]
public class SwordData
{
    public string name;
    [Header("Swing:")]
    public float swingSpeed;
    public float swingWidth;
    public float swingCoolDown;
    [Header("Damage:")]
    public float swordPhysicDamage;
    [Header("KnockBack:")]
    public float knockBackSpeed;
    public float knockBackDuration;
    [Header("Attack dash:")]
    public float dashSpeed;
    public float dashDuration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;

    public float DashDistance { get => dashSpeed * dashDuration; }
    public float Damage { get => swordPhysicDamage; }

    public SwordData() { }
    public SwordData(SwordDataSO SwordSO)
    {
        this.name = SwordSO.name;
        this.swingCoolDown = SwordSO.swingCoolDown;
        this.swingWidth = SwordSO.swingWidth;
        this.swingSpeed = SwordSO.swingSpeed;
        this.swordPhysicDamage = SwordSO.SwordPhysicDamage;
        this.knockBackSpeed = SwordSO.knockBackSpeed;
        this.knockBackDuration = SwordSO.knockBackDuration;
        this.dashSpeed = SwordSO.dashSpeed;
        this.dashDuration = SwordSO.dashDuration;
        this.swordScale = SwordSO.swordScale;
        this.isBlockable = SwordSO.isBlockable;
    }
}

[CreateAssetMenu(menuName = "SwordData", fileName = "New SwordData")]
public class SwordDataSO : ScriptableObject
{
    [Header("Swing:")]
    public float swingSpeed;
    public float swingWidth;
    public float swingCoolDown;
    [Header("Damage:")]
    public float SwordPhysicDamage;
    [Header("KnockBack:")]
    public float knockBackSpeed;
    public float knockBackDuration;
    [Header("Attack dash:")]
    public float dashSpeed;
    public float dashDuration;
    [Header("Other:")]
    public Vector3 swordScale;
    public bool isBlockable = true;
    public float DashDistance { get => dashSpeed * dashDuration; }

    public void SetData(SwordData data)
    {
        this.name = data.name;
        this.swingCoolDown = data.swingCoolDown;
        this.swingWidth = data.swingWidth;
        this.swingSpeed = data.swingSpeed;
        this.SwordPhysicDamage = data.swordPhysicDamage;
        this.knockBackSpeed = data.knockBackSpeed;
        this.knockBackDuration = data.knockBackDuration;
        this.dashSpeed = data.dashSpeed;
        this.dashDuration = data.dashDuration;
        this.swordScale = data.swordScale;
        this.isBlockable = data.isBlockable;
    }
}
