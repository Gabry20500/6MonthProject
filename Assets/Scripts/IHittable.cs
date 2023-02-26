using UnityEngine;
using System.Collections;
public interface IHittable 
{
    public void OnHit(float damage, Vector3 knockBackDir, SwordData sword) { }
    public void OnHit(float damage, Vector3 knockBackDir, EnemyData enemy) { }
    void OnClash(Vector3 knockBackDir, SwordData sword) { }
    void OnClash(Vector3 knockBackDir, EnemyData enemy) { }
    public IEnumerator KnockbackCoroutine(float cooldown) { yield return null; }

}
