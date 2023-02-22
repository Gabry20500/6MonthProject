using UnityEngine;

public class EnemyState
{
    protected EnemyStateProcessor context;
    protected EnemyAI enemy;

    public EnemyState(EnemyStateProcessor context, EnemyAI enemy)
    {
        this.context = context;
        this.enemy = enemy;
    }
    virtual public void Update()
    {
        
    }
}

public class IdleState : EnemyState
{
    public IdleState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }

    public override void Update()
    {
        enemy.distance = Vector3.Distance(enemy.targetPlayer.position, enemy.transform.position);
        if (enemy.distance <= enemy.sightDistance && enemy.distance > enemy.attackReach)
        {
            context.currentState = context.seekState;
        }
        else if (enemy.distance > enemy.sightDistance)
        {
            return;
        }
    }
}

public class SeekState : EnemyState
{
    private bool seeking = false;
    public SeekState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }
    public override void Update()
    {
        enemy.distance = Vector3.Distance(enemy.targetPlayer.position, enemy.transform.position);

        if(enemy.distance < enemy.attackReach)
        {
            seeking = false;
            enemy._agent.velocity = Vector3.zero;
            context.currentState = context.attackState;
        }
        else if (enemy.distance <= enemy.sightDistance && enemy.distance > enemy.attackReach)
        {
            if(seeking == false)
            {
                enemy._agent.SetDestination(enemy.targetPlayer.position);
                seeking = true;
            }
        }
        else if (enemy.distance > enemy.sightDistance)
        {
            seeking = false;
            enemy._agent.SetDestination(enemy.transform.position);
            context.currentState = context.idleState;
        }
    }
}

public class AttackState : EnemyState
{
    private bool attacking = false;
    public AttackState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }
    public override void Update()
    {
        enemy.distance = Vector3.Distance(enemy.targetPlayer.position, enemy.transform.position);

        if (enemy.distance < enemy.attackReach && attacking == false)
        {
            
        }
        else if (enemy.distance > enemy.attackReach)
        {
            attacking = false;
            context.currentState = context.seekState;
        }
    }
}
