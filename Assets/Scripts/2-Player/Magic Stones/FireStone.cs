using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FireStone", menuName = "Stone/New FireStone", order = 1)]
public class FireStone : Stone
{
    [SerializeField] float tickDamagePercentage = 25.0f; 
    [SerializeField] float duration = 5.0f;
    [SerializeField] float tickTime = 1.0f;
    public override void OnEnemyHitted(Sword sword,EnemyAI enemy) 
    {
        enemy.StartCoroutine(Burning_Routine(sword.swordData.Damage/(100/tickDamagePercentage), enemy));
    }

    private IEnumerator Burning_Routine(float damage, EnemyAI enemy)
    {
        float buffer = 0.0f;
        float tickBuff = 0.0f;
        while (buffer < duration)
        {
            if(tickBuff > tickTime)
            {
                tickBuff = 0.0f;
                enemy.Enemy.TakeDamage(damage);
            }
            buffer += Time.deltaTime;
            tickBuff += Time.deltaTime;
            yield return null;
        }
    }
}
