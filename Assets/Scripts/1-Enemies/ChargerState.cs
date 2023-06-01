using DG.Tweening;
using UnityEngine;

public class Charger_Idle_State : ChargerState
{
    public Charger_Idle_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){ }

    public override void OnStateEnter()
    {
        
    }

    public override void Update()
    {
        if(enemy.nav_Agent.isStopped == false) { enemy.nav_Agent.isStopped = true; }

        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.chenemy_Data.sightDistance)
        {
            processor.ChseekState.OnStateEnter();
            processor.currentState = processor.ChseekState;
        }
        else if (enemy.Distance > enemy.chenemy_Data.sightDistance)
        {
            return;
        }
    }
}
public class Charger_SeekState : ChargerState
{
    public Charger_SeekState(ChargerStateProcessor context, ChargerAI enemy, EnemySword sword) : base(context, enemy) { this.sword = sword; }
    public override void Update()
    {
        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);

        targetDir = new Vector3(enemy.nav_Agent.velocity.x, 0.0f, enemy.nav_Agent.velocity.z).normalized;
        targetRot = Quaternion.LookRotation(-targetDir);
        sword.transform.parent.transform.DOLocalRotateQuaternion(targetRot, 0.01f);

        if (enemy.Distance < enemy.chenemy_Data.attackReach)
        {
            enemy.nav_Agent.isStopped = true;
            
            processor.ChargingState.OnStateEnter();
            processor.currentState = processor.ChargingState;
        }
        else if (enemy.Distance < enemy.chenemy_Data.sightDistance)
        {
            enemy.nav_Agent.isStopped = false;
            enemy.nav_Agent.SetDestination(enemy.target.position);
            // Debug.DrawRay(enemy.target.position, enemy.nav_Agent.velocity, Color.red, 1.0f);
            // Debug.DrawRay(enemy.target.position, Vector3.up, Color.red, 1.0f);
        }
        else if (enemy.Distance > enemy.chenemy_Data.sightDistance)
        {
            enemy.nav_Agent.SetDestination(enemy.transform.position);
            processor.currentState = processor.IdleState;
        }
    }
}

public class Charger_Charging_State : ChargerState
{
    private float buffer = 0.0f;
    private Vector3 dir;
    public Charger_Charging_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy) {}
    public override void OnStateEnter()
    {
        buffer = 0.0f;
        enemy.pointer.SetActive(true);
        enemy.nav_Agent.isStopped = true;
        enemy.ai_Animator.charging = true;
        enemy.ai_Animator.StartCoroutine(enemy.ai_Animator.Charging_Color(enemy.chenemy_Data.chargeTime));
    }
    public override void Update()
    {
        dir = -(enemy.target.position - enemy.transform.position).normalized;
        enemy.pointer.transform.forward = new Vector3(dir.x, dir.y, dir.z);
        if (buffer < enemy.chenemy_Data.chargeTime)
        {
            buffer += Time.deltaTime;
        }
        else
        {
            processor.DashState.destination = enemy.target.position;
            enemy.ai_Animator.charging = false;
            processor.DashState.OnStateEnter();         
            processor.currentState = processor.DashState;
        }
    }
}

public class Dash_State : ChargerState
{
    public Vector3 destination;
    public Vector3 dir;
    private Ray ray;
    private RaycastHit hit;
    public Dash_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){}

    float dash_Speed;
    float dash_Time;
    float buffer = 0.0f;

    public override void OnStateEnter()
    {
        buffer = 0.0f;
        enemy.pointer.SetActive(false);
        enemy.nav_Agent.isStopped = true;
        enemy.IsAttacking = true;
        dash_Speed = enemy.chenemy_Data.dashSpeed;
        dash_Time = enemy.chenemy_Data.dashTime;
        dir = (destination - enemy.transform.position).normalized;

        ray = new Ray(enemy.transform.position, dir * dash_Speed);
        if (Physics.Raycast(ray, out hit, LayerMask.NameToLayer("Wall")))
        {
            destination = new Vector3(hit.point.x, enemy.transform.position.y, hit.point.z);
        }
        enemy.GetComponent<AudioSource>().clip = enemy.dashSound;
        enemy.GetComponent<AudioSource>().Play();
        enemy.ai_Animator.dashing = true;
    }

    public override void Update()
    {
        if (Mathf.Abs((destination - enemy.transform.position).magnitude) > 4f)
        {
            enemy.transform.position += dash_Speed * Time.deltaTime * dir;
        }
        else
        {
            processor.CoolDownState.OnStateEnter();
            processor.currentState = processor.CoolDownState;
            enemy.ai_Animator.dashing = false;
        }
        buffer += Time.deltaTime;
    }
}
public class CoolDown_State : ChargerState
{
    float buffer = 0.0f;
    public CoolDown_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){}
    public override void OnStateEnter()
    {
        enemy.IsAttacking = false;
        buffer = 0.0f;
    }
    public override void Update()
    {
        if (buffer < enemy.chenemy_Data.stun_Time)
        {
            buffer += Time.deltaTime;
        }
        else
        {
            processor.IdleState.OnStateEnter();
            processor.currentState = processor.IdleState;
        }
    }
}
