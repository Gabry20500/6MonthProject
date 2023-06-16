using UnityEngine;

[CreateAssetMenu(fileName = "EarthStone", menuName = "Stone/New EarthStone", order = 4)]
public class EarthStone : Stone
{
    [SerializeField] private float damageBoostPercentage = 50.0f;
    [SerializeField] private float swingRangeBoostPercentage = 10.0f;
    [SerializeField] private float swingSlowdownPercentage = 20.0f;
    public override void OnSelected(Sword sword) 
    {
        base.OnSelected(sword);
        sword.swordData.Damage += (sword.swordData.Damage / 100) * damageBoostPercentage;
        sword.swordData.swingWidth += (sword.swordData.swingWidth / 100) * swingRangeBoostPercentage;
        sword.swordData.swingSpeed -= (sword.swordData.swingSpeed / 100) * swingSlowdownPercentage;
    }
    public override void OnDeselected(Sword sword) 
    {
        sword.swordData.Damage = 
            (sword.swordData.Damage * 100) / (100 + damageBoostPercentage);
        sword.swordData.swingWidth =
            (sword.swordData.swingWidth * 100) / (100 + swingRangeBoostPercentage);
        sword.swordData.swingSpeed =
            (sword.swordData.swingSpeed * 100) / (100 - swingSlowdownPercentage);
    }

    public override void OnEnemyHitted(Sword sword, EnemyAI enemy)
    {
        sword.UseMana();
    }
}
