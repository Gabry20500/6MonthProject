using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterStone", menuName = "Stone/New WaterStone", order = 2)]
public class WaterStone : Stone
{
    [SerializeField] float duration = 5.0f;
    [SerializeField] float slowingPercentage = 15.0f;
    public override void OnEnemyHitted(Sword sword, EnemyAI enemy) 
    {
        enemy.StartCoroutine(Slowing_Routine(enemy));
        sword.UseMana();
    }

    private IEnumerator Slowing_Routine(EnemyAI enemy)
    {
        float buffer = 0.0f;
        float original = enemy.nav_Agent.speed;
        enemy.nav_Agent.speed -= (enemy.nav_Agent.speed / 100) * slowingPercentage;
        while (buffer < duration)
        {
            buffer += Time.deltaTime;
            yield return null;
        }
        enemy.nav_Agent.speed = original;
    }
}
