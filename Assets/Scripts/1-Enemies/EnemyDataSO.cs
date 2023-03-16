using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [Header("Attack parameters:")]
    public float swordDamage;
    public float attackRate;
    [Header("KnockBack parameters:")]
    public float knockSpeed;
    public float knockDuration;
    [Header("AI parameters:")]
    public float sightDistance;
    public float attackReach;
    [Header("Freeeze frames:")]
    public float freeze_Intensity;
    public float freeze_Duration;
    public EnemyData() { }
    public EnemyData(EnemyDataSO enemySO)
    {
        swordDamage = enemySO.swordDamage;
        attackRate = enemySO.attackRate;
        knockSpeed = enemySO.knockSpeed;
        knockDuration = enemySO.knockDuration;
        sightDistance = enemySO.sightDistance;
        attackReach = enemySO.attackReach;
        freeze_Intensity = enemySO.freeze_Intensity;
        freeze_Duration = enemySO.freeze_Duration;
    }
}

[CreateAssetMenu(menuName = "EnemyData", fileName = "New EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Attack parameters:")]
    public float swordDamage;
    public float attackRate;
    [Header("KnockBack parameters:")]
    public float knockSpeed;
    public float knockDuration;
    [Header("AI parameters:")]
    public float sightDistance;
    public float attackReach;
    [Header("Freeeze frames:")]
    public float freeze_Intensity;
    public float freeze_Duration;

    public void SetData(EnemyData data)
    {
        swordDamage = data.swordDamage;
        attackRate = data.attackRate;
        knockSpeed = data.knockSpeed;
        knockDuration = data.knockDuration;
        sightDistance = data.sightDistance;
        attackReach = data.attackReach;
        freeze_Intensity = data.freeze_Intensity;
        freeze_Duration = data.freeze_Duration;
    }
}