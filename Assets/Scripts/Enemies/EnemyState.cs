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

    public virtual void OnStateEnter() { }
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
    private EnemySword sword;
    public AttackState(EnemyStateProcessor context, EnemyAI enemy, EnemySword sword) : base(context, enemy) { this.sword = sword; }
    public override void Update()
    {
        enemy.distance = Vector3.Distance(enemy.targetPlayer.position, enemy.transform.position);

        if (enemy.distance < enemy.attackReach && attacking == false)
        {
            enemy._animator.SetBool("Attack", true);
            sword.IsAttacking = true;
            attacking = true;
        }
        else if (enemy.distance > enemy.attackReach)
        {
            enemy._animator.SetBool("Attack", false);
            sword.IsAttacking = false;
            attacking = false;
            context.currentState = context.seekState;
        }
    }
}

public class HittedState : EnemyState
{
    Vector3 hitDir;
    float hitSpeed;
    float duration;

    float buffer = 0.0f;
    public HittedState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) {}

    public void Init(Vector3 hitDir, float hitSpeed, float duration)
    {
        buffer = 0.0f;
        this.duration = duration;
        this.hitSpeed = hitSpeed;
        this.hitDir = hitDir;

    }

    public override void OnStateEnter()
    {
        enemy._agent.SetDestination(enemy.transform.position);
    }

    public override void Update()
    {
        if(buffer < duration)
        {

        }
        else
        {
            context.currentState = context.idleState;
        }
        buffer += Time.deltaTime;
    }
}
