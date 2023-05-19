using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "FireStone", menuName = "Stone/New FireStone", order = 1)]
public class FireStone : Stone
{


    public override void OnSelected(Sword sword) { }
    public override void OnDeselected(Sword sword) { }
    public override void OnEnemyHitted(EnemyAI enemy) { }

}
