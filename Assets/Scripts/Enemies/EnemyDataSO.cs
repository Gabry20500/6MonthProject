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

    public EnemyData() { }
    public EnemyData(EnemyDataSO enemySO)
    {
        this.swordDamage = enemySO.swordDamage;
        this.attackRate = enemySO.attackRate;
        this.knockSpeed = enemySO.knockSpeed;
        this.knockDuration = enemySO.knockDuration;
        this.sightDistance = enemySO.sightDistance;
        this.attackReach = enemySO.attackReach;
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

    public void SetData(EnemyData data)
    {
        this.swordDamage = data.swordDamage;
        this.attackRate = data.attackRate;
        this.knockSpeed = data.knockSpeed;
        this.knockDuration = data.knockDuration;
        this.sightDistance = data.sightDistance;
        this.attackReach = data.attackReach;
    }

}