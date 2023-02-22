using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateProcessor
{
    public EnemyAI enemy;


    public IdleState idleState;
    public SeekState seekState;
    public AttackState attackState;

    public EnemyState currentState;

    

    public EnemyStateProcessor(EnemyAI context)
    {
        enemy = context;
        idleState = new IdleState(this, enemy);
        seekState = new SeekState(this, enemy);
        attackState = new AttackState(this, enemy);
    }

    public void Init()
    {
        currentState = idleState;
    }

    public void Update()
    {
        currentState.Update();
    }
}
