using UnityEngine;
using System.Collections;
public interface IHittable 
{
    //Potrei estrarre una classe comune per swordData ed enemy data che contenga i valori per gli hit e i clas, oppure una interfaccia e chiamarla IHitter
    public void OnHit(float damage, Vector3 knockBackDir, Player_SwordData sword) { }
    public void OnHit(float damage, Vector3 knockBackDir, EnemyData enemy) { }
    public IEnumerator KnockbackCoroutine(float cooldown) { yield return null; }
}

public interface IClashable
{
    //Potrei estrarre una classe comune per swordData ed enemy data che contenga i valori per gli hit e i clas, oppure una interfaccia e chiamarla IHitter
    void OnClash(Vector3 knockBackDir, Player_SwordData sword) { }
    void OnClash(Vector3 knockBackDir, EnemyData enemy) { }
}

