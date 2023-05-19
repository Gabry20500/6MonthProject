using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaterStone", menuName = "Stone/New WaterStone", order = 2)]
public class WaterStone : Stone
{

    public override void OnSelected(Sword sword) { }
    public override void OnDeselected(Sword sword) { }
    public override void OnEnemyHitted(EnemyAI enemy) { }
}
