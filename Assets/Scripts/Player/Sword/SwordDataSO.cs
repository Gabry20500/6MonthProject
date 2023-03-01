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

    public SwordData() { }
    public SwordData(SwordDataSO SwordSO)
    {
        this.name = SwordSO.name;
        this.swingCoolDown = SwordSO.swingCoolDown;
        this.swingWidth = SwordSO.swingWidth;
        this.swingSpeed = SwordSO.swingSpeed;
        this.physicDamage = SwordSO.physicDamage;
        this.knockSpeed = SwordSO.knockSpeed;
        this.knockDuration = SwordSO.knockDuration;
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

    public void SetData(SwordData data)
    {
        this.name = data.name;
        this.swingCoolDown = data.swingCoolDown;
        this.swingWidth = data.swingWidth;
        this.swingSpeed = data.swingSpeed;
        this.physicDamage = data.physicDamage;
        this.knockSpeed = data.knockSpeed;
        this.knockDuration = data.knockDuration;
        this.dashSpeed = data.dashSpeed;
        this.dashDuration = data.dashDuration;
        this.swordScale = data.swordScale;
        this.isBlockable = data.isBlockable;
    }
}
