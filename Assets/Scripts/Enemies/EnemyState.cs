using UnityEngine;

public class EnemyState
{
    protected EnemyStateProcessor processor;
    protected EnemyAI enemy;

    public EnemyState(EnemyStateProcessor context, EnemyAI enemy)
    {
        this.processor = context;
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
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.enemyData.sightDistance )
        {
            processor.currentState = processor.seekState;
        }
        else if (enemy.Distance > enemy.enemyData.sightDistance)
        {
            return;
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
    public SeekState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }
    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.enemyData.attackReach)
        {
            enemy.agent.isStopped = true;
            processor.currentState = processor.attackState;
        }
        else if (enemy.Distance < enemy.enemyData.sightDistance)
        {
                enemy.agent.isStopped = false;
                enemy.agent.SetDestination(enemy.target.position);              
        }
        else if (enemy.Distance > enemy.enemyData.sightDistance)
        {
            enemy.agent.SetDestination(enemy.transform.position);
            processor.currentState = processor.idleState;
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
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.enemyData.attackReach && attacking == false)
        {
            enemy.animator.SetBool("Attack", true);
            sword.IsAttacking = true;
            attacking = true;
        }
        if (enemy.Distance < enemy.enemyData.attackReach && attacking == true)
        {
            Vector3 targetDir = (enemy.target.position - enemy.transform.position).normalized;
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            Quaternion nextRotation = Quaternion.Lerp(enemy.transform.localRotation, targetRot, 0.1f);
            sword.transform.parent.transform.forward = -targetDir;
        }
        else if (enemy.Distance > enemy.enemyData.attackReach)
        {
            enemy.animator.SetBool("Attack", false);
            sword.IsAttacking = false;
            attacking = false;
            processor.currentState = processor.idleState;
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
            processor.currentState = processor.idleState;
            enemy.agent.isStopped = false;
        }
        buffer += Time.deltaTime;
    }
}
