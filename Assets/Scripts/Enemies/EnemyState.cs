using UnityEngine;
using DG.Tweening;

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
        if (enemy.Distance < enemy.enemy_Data.sightDistance )
        {
            processor.currentState = processor.seekState;
        }
        else if (enemy.Distance > enemy.enemy_Data.sightDistance)
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
        if (enemy.Distance < enemy.enemy_Data.attackReach)
        {
            enemy.enemy_Agent.isStopped = true;
            processor.currentState = processor.attackState;
        }
        else if (enemy.Distance < enemy.enemy_Data.sightDistance)
        {
            enemy.enemy_Agent.isStopped = false;
            enemy.enemy_Agent.SetDestination(enemy.target.position);
            Debug.DrawRay(enemy.target.position, enemy.enemy_Agent.velocity, Color.red, 1.0f);
            Debug.DrawRay(enemy.target.position, Vector3.up, Color.red, 1.0f);
        }
        else if (enemy.Distance > enemy.enemy_Data.sightDistance)
        {
            enemy.enemy_Agent.SetDestination(enemy.transform.position);
            processor.currentState = processor.idleState;
        }
    }
}

public class AttackState : EnemyState
{
    private bool attacking = false;
    private EnemySword sword;

    Vector3 targetDir;
    Quaternion targetRot;
    public AttackState(EnemyStateProcessor context, EnemyAI enemy, EnemySword sword) : base(context, enemy) { this.sword = sword; }
    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.enemy_Data.attackReach && attacking == false)
        {
            enemy.enemy_Animator.SetBool("Attack", true);
            sword.IsAttacking = true;
            attacking = true;
        }
        if (enemy.Distance < enemy.enemy_Data.attackReach && attacking == true)
        {
            targetDir = (enemy.target.position - enemy.transform.position).normalized;
            targetRot = Quaternion.LookRotation(-targetDir);
            sword.transform.parent.transform.DOLocalRotateQuaternion(targetRot, 0.1f).SetEase(Ease.InSine);          
        }
        else if (enemy.Distance > enemy.enemy_Data.attackReach)
        {
            enemy.enemy_Animator.SetBool("Attack", false);
            sword.IsAttacking = false;
            attacking = false;
            processor.currentState = processor.idleState;
        }
    }

    public override void OnStateExit()
    {
        enemy.enemy_Animator.SetBool("Attack", false);
        sword.IsAttacking = false;
        attacking = false;
    }
}


public class KnockState : EnemyState
{
    Vector3 knock_Dir;
    float knock_Speed;
    float knock_Time;

    float buffer = 0.0f;
    public KnockState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) {}

    public void Init(Vector3 hit_Dir, float knock_Speed, float duration)
    {
        flag = false;
        buffer = 0.0f;
        this.knock_Time = duration;
        this.knock_Speed = knock_Speed;
        this.knock_Dir = hit_Dir;
    }

    public override void OnStateEnter()
    {
        enemy.enemy_Agent.isStopped = true;
    }

    bool flag = false;
    public override void Update()
    {
        if(flag == false) { OnStateEnter(); flag = true; }
        if(buffer < knock_Time)
        {
            enemy.transform.position += knock_Speed * Time.deltaTime * knock_Dir;
        }
        else
        {
            processor.currentState = processor.idleState;
            enemy.enemy_Agent.isStopped = false;
        }
        buffer += Time.deltaTime;
    }
}
