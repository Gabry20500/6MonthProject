
using UnityEngine;

[System.Serializable]
public class ChargerData : EnemyData
{
    [Header("Charger variables")]
    public float chargeTime;
    public float dashSpeed;
    public float dashTime;


    public ChargerData() { }
    public ChargerData(ChargerDataSO chargerSO)
    {
        HP = chargerSO.HP;
        max_HP = chargerSO.max_HP;
        swordDamage = chargerSO.swordDamage;
        attackRate = chargerSO.attackRate;
        knockSpeed = chargerSO.knockSpeed;
        knockDuration = chargerSO.knockDuration;
        sightDistance = chargerSO.sightDistance;
        attackReach = chargerSO.attackReach;
        freeze_Intensity = chargerSO.freeze_Intensity;
        freeze_Duration = chargerSO.freeze_Duration;
        stun_Time = chargerSO.stun_Time;
        chargeTime = chargerSO.chargeTime;
        dashSpeed = chargerSO.dashSpeed;
        dashTime = chargerSO.dashTime;
    }
}

[CreateAssetMenu(menuName = "ChargerData", fileName = "New ChargerData")]
public class ChargerDataSO : EnemyDataSO
{
    [Header("Charger variables")]
    public float chargeTime;
    public float dashSpeed;
    public float dashTime;

    public override void SetData(EnemyData data)
    {
        HP = data.HP;
        max_HP = data.max_HP;
        swordDamage = data.swordDamage;
        attackRate = data.attackRate;
        knockSpeed = data.knockSpeed;
        knockDuration = data.knockDuration;
        sightDistance = data.sightDistance;
        attackReach = data.attackReach;
        freeze_Intensity = data.freeze_Intensity;
        freeze_Duration = data.freeze_Duration;
        stun_Time = data.stun_Time;
        ChargerData data2 = (ChargerData)data;
        chargeTime = data2.chargeTime;
        dashSpeed = data2.dashSpeed;
        dashTime = data2.dashTime;
    }
}
