using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirStone", menuName = "Stone/New AirStone", order = 3)]
public class AirStone : Stone
{

    public override void OnSelected(Sword sword) { }
    public override void OnDeselected(Sword sword) { }
    public override void OnEnemyHitted(EnemyAI enemy) { }
}
