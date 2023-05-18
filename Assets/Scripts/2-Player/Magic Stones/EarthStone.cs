using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarthStone", menuName = "Stone/New EarthStone", order = 4)]
public class EarthStone : Stone
{

    public override void OnSelected(Sword sword) { }
    public override void OnDeselected(Sword sword) { }
    public override void OnEnemyHitted(EnemyAI enemy) { }
}
