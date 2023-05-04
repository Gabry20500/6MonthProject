using UnityEngine;
using DG.Tweening;

public class State
{
    protected EnemySword sword;

    protected Vector3 targetDir;
    protected Quaternion targetRot;

    public virtual void OnStateEnter() { }
    virtual public void Update() { }
    public virtual void OnStateExit() { }
}
public class EnemyState : State
{
    protected EnemyStateProcessor processor;
    protected EnemyAI enemy;

    public EnemyState(EnemyStateProcessor context, EnemyAI enemy)
    {
        this.processor = context;
        this.enemy = enemy;
    }
}

public class ChargerState : State
{
    protected ChargerAI enemy;
    protected ChargerStateProcessor processor;
    public ChargerState(ChargerStateProcessor context, ChargerAI enemy)
    {
        this.processor = context;
        this.enemy = enemy;
    }
}


public class IdleState : EnemyState
{
public IdleState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) {  }

    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.Enemy_Data.sightDistance )
        {
            processor.currentState = processor.SeekState;
        }
        else if (enemy.Distance > enemy.Enemy_Data.sightDistance)
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
    public SeekState(EnemyStateProcessor context, EnemyAI enemy, EnemySword sword) : base(context, enemy) { this.sword = sword; }
    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);

        targetDir = new Vector3(enemy.nav_Agent.velocity.x, 0.0f, enemy.nav_Agent.velocity.z).normalized; 
        targetRot = Quaternion.LookRotation(-targetDir);
        sword.transform.parent.transform.DOLocalRotateQuaternion(targetRot, 0.01f);

        if (enemy.Distance < enemy.Enemy_Data.attackReach)
        {
            enemy.nav_Agent.isStopped = true;
            processor.currentState = processor.AttackState;
        }
        else if (enemy.Distance < enemy.Enemy_Data.sightDistance)
        {
            enemy.nav_Agent.isStopped = false;
            enemy.nav_Agent.SetDestination(enemy.target.position);
            // Debug.DrawRay(enemy.target.position, enemy.nav_Agent.velocity, Color.red, 1.0f);
            // Debug.DrawRay(enemy.target.position, Vector3.up, Color.red, 1.0f);
        }
        else if (enemy.Distance > enemy.Enemy_Data.sightDistance)
        {
            enemy.nav_Agent.SetDestination(enemy.transform.position);
            processor.currentState = processor.IdleState;
        }
    }
}

public class AttackState : EnemyState
{
    private bool attacking = false;
    
    public AttackState(EnemyStateProcessor context, EnemyAI enemy, EnemySword sword) : base(context, enemy) { this.sword = sword; }
    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.Enemy_Data.attackReach && attacking == false)
        {
            enemy.enemy_Animator.SetBool("Attack", true);
            sword.IsAttacking = true;
            attacking = true;
        }
        if (enemy.Distance < enemy.Enemy_Data.attackReach && attacking == true)
        {
            targetDir = (enemy.target.position - enemy.transform.position).normalized;
            targetRot = Quaternion.LookRotation(-targetDir);
            sword.transform.parent.transform.DOLocalRotateQuaternion(targetRot, 0.1f).SetEase(Ease.InSine);          
        }
        else if (enemy.Distance > enemy.Enemy_Data.attackReach)
        {
            enemy.enemy_Animator.SetBool("Attack", false);
            sword.IsAttacking = false;
            attacking = false;
            processor.currentState = processor.IdleState;
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
        enemy.nav_Agent.isStopped = true;
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
            processor.currentState = processor.StunState;
            enemy.nav_Agent.isStopped = false;
        }
        buffer += Time.deltaTime;
    }
}


public class StunState : EnemyState
{
    float buffer = 0.0f;
    public StunState(EnemyStateProcessor context, EnemyAI enemy) : base(context, enemy) { }


    public override void OnStateEnter()
    {
        enemy.nav_Agent.isStopped = true;
    }

    bool flag = false;
    public override void Update()
    {
        if (flag == false) { OnStateEnter(); flag = true; }
        if (buffer < enemy.Enemy_Data.stun_Time)
        {
            buffer += Time.deltaTime;
        }
        else
        {
            buffer = 0.0f;
            processor.currentState = processor.IdleState;
            enemy.nav_Agent.isStopped = false;
        }

    }
}