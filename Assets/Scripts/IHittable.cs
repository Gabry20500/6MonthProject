using UnityEngine;
using System.Collections;
public interface IHittable 
{
    public void OnHit(Vector3 knockBackDir, SwordData sword) { }
    public void OnHit(Vector3 knockBackDir, EnemyData enemy) { }
    public IEnumerator KnockbackCoroutine(float cooldown) { yield return null; }

}
