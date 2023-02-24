using UnityEngine;

[System.Serializable]
public class SwordData
{
    public string name;
    [Header("Swing:")]
    public float swingCoolDown;
    public float swingWidth;
    public float swingSpeed;
    [Header("Damage:")]
    public float swordPhysicDamage;
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
    public float swingCoolDown;
    public float swingWidth;
    public float swingSpeed;
    [Header("Damage:")]
    public float SwordPhysicDamage;
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
        this.dashSpeed = data.dashSpeed;
        this.dashDuration = data.dashDuration;
        this.swordScale = data.swordScale;
        this.isBlockable = data.isBlockable;
    }
}
