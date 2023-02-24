using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData
{
    [Header("Attack parameters:")]
    public float swordDamage;
    public float attackRate;
    [Header("KnockBack parameters:")]
    public float knockBackSpeed;
    public float knockBackDuration;
    [Header("AI parameters:")]
    public float sightDistance;
    public float attackReach;

    public EnemyData() { }
    public EnemyData(EnemyDataSO enemySO)
    {
        this.swordDamage = enemySO.swordDamage;
        this.attackRate = enemySO.attackRate;
        this.knockBackSpeed = enemySO.knockBackSpeed;
        this.knockBackDuration = enemySO.knockBackDuration;
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
    public float knockBackSpeed;
    public float knockBackDuration;
    [Header("AI parameters:")]
    public float sightDistance;
    public float attackReach;

    public void SetData(EnemyData data)
    {
        this.swordDamage = data.swordDamage;
        this.attackRate = data.attackRate;
        this.knockBackSpeed = data.knockBackSpeed;
        this.knockBackDuration = data.knockBackDuration;
        this.sightDistance = data.sightDistance;
        this.attackReach = data.attackReach;
    }

}