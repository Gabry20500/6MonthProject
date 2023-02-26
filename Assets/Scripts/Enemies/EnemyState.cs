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
    virtual public void Update() { }
    public virtual void OnStateExit() { }
}


public class IdleState : EnemyState
{
public IdleState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) {  }

    public override void Update()
    {
        Debug.Log(this.GetType().ToString());
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance <= enemy.enemyData.sightDistance && enemy.Distance > enemy.enemyData.attackReach)
        {
            context.currentState = context.seekState;
        }
        else if (enemy.Distance > enemy.enemyData.sightDistance)
        {
            return;
            //context.currentState = context.wanderingState;
        }
    }
}

//public class WanderingState : EnemyState
//{
//    bool wandering = false;
//    float wanderBuffer = 0.0f;
//    float wanderTime = 10.0f;
//    float wanderRange = 10.0f;
//    Vector3 wanderDestination;

//    public WanderingState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }

//    public override void Update()
//    {
//        Debug.Log(this.GetType().ToString());
//        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);

//        if (enemy.Distance > enemy.enemyData.sightDistance)
//        {
//            if(wandering == false || wanderBuffer > wanderTime)
//            {
//                wandering = true;
//                wanderBuffer = 0.0f;
//                wanderDestination = new Vector3(enemy.gameObject.transform.position.x + Random.Range(0, wanderRange), 0.0f,
//                                                enemy.gameObject.transform.position.z + Random.Range(0, wanderRange));
//                enemy.agent.SetDestination(wanderDestination);
//            }
//            else
//            {
//                wanderBuffer += Time.deltaTime;
//            }
//        }
//        else if (enemy.Distance < enemy.enemyData.sightDistance)
//        {
//            wandering = false;
//            wanderBuffer = 0.0f;
//            enemy.agent.isStopped = true;
//            context.currentState = context.seekState;
//        }
//    }
//}


public class SeekState : EnemyState
{
    private bool seeking = false;
    public SeekState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }
    public override void Update()
    {
        //Debug.Log(this.GetType().ToString());
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);

        if(enemy.Distance < enemy.enemyData.attackReach)
        {
            seeking = false;
            enemy.agent.isStopped = true;
            context.currentState = context.attackState;
        }
        else if (enemy.Distance < enemy.enemyData.sightDistance && enemy.Distance > enemy.enemyData.attackReach)
        {
            if(seeking == false)
            {
                enemy.agent.isStopped = false;
                enemy.agent.SetDestination(enemy.target.position);
                seeking = true;
            }
        }
        else if (enemy.Distance > enemy.enemyData.sightDistance)
        {
            seeking = false;
            enemy.agent.SetDestination(enemy.transform.position);
            context.currentState = context.idleState;
            //context.currentState = context.wanderingState;
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
        Debug.Log(this.GetType().ToString());
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.enemyData.attackReach && attacking == false)
        {
            enemy.animator.SetBool("Attack", true);
            sword.IsAttacking = true;
            attacking = true;
        }
        else if (enemy.Distance > enemy.enemyData.attackReach)
        {
            enemy.animator.SetBool("Attack", false);
            sword.IsAttacking = false;
            attacking = false;
            context.currentState = context.seekState;
        }
    }

    public override void OnStateExit()
    {
        enemy.animator.SetBool("Attack", false);
        sword.IsAttacking = false;
        attacking = false;
    }
}


public class KnockBackState : EnemyState
{
    Vector3 knockDir;
    float knockSpeed;
    float kncockDuration;

    float buffer = 0.0f;
    public KnockBackState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) {}

    public void Init(Vector3 hitDir, float knockSpeed, float duration)
    {
        flag = false;
        buffer = 0.0f;
        this.kncockDuration = duration;
        this.knockSpeed = knockSpeed;
        this.knockDir = hitDir;

    }

    public override void OnStateEnter()
    {
        enemy.agent.isStopped = true;
    }

    bool flag = false;
    public override void Update()
    {
        if(flag == false) { OnStateEnter(); flag = true; }
        if(buffer < kncockDuration)
        {
            enemy.transform.position += knockDir * knockSpeed * Time.deltaTime;
        }
        else
        {
            context.currentState = context.idleState;
            //context.currentState = context.wanderingState;
            enemy.agent.isStopped = false;
        }
        buffer += Time.deltaTime;
    }
}
