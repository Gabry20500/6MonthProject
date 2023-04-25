using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Charger_Idle_State : ChargerState
{
    public Charger_Idle_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){ }

    public override void OnStateEnter()
    {
        enemy.nav_Agent.isStopped = true;
    }
    public override void Update()
    {
        Vector3 dist = enemy.transform.position - enemy.transform.position;
        dist.Normalize();
        Debug.DrawLine(enemy.transform.position, dist * enemy.chenemy_Data.sightDistance, Color.red, 0.016f);

        enemy.Distance = Vector3.Distance(enemy.target.position, enemy.transform.position);
        if (enemy.Distance < enemy.Enemy_Data.sightDistance)
        {
            //enemy.nav_Agent.isStopped = false;
            processor.DashState.destination = enemy.target.position;
            processor.ChargingState.OnStateEnter();
            processor.currentState = processor.ChargingState;
            
        }
        else if (enemy.Distance > enemy.Enemy_Data.sightDistance)
        {
            return;
        }
    }
}

public class Charger_Charging_State : ChargerState
{
    float buffer = 0.0f;
    public Charger_Charging_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy) {}
    public override void OnStateEnter()
    {
        buffer = 0.0f;
        enemy.nav_Agent.isStopped = true;
    }
    public override void Update()
    {
        if (buffer < enemy.chenemy_Data.chargeTime)
        {
            buffer += Time.deltaTime;
        }
        else
        {
            processor.DashState.OnStateEnter();
            processor.currentState = processor.DashState;
        }
    }
}

public class Dash_State : ChargerState
{
    public Vector3 destination;
    public Dash_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){}

    float dash_Speed;
    float dash_Time;
    float buffer = 0.0f;

    public override void OnStateEnter()
    {
        buffer = 0.0f;
        enemy.nav_Agent.isStopped = true;
        dash_Speed = enemy.chenemy_Data.dashSpeed;
        dash_Time = enemy.chenemy_Data.dashTime;
    }

    public override void Update()
    {
        Debug.Log("Dash");
        if (buffer < enemy.chenemy_Data.dashTime && Mathf.Abs((destination-enemy.transform.position).magnitude) > 0.1f)
        {
            enemy.transform.position += dash_Speed * Time.deltaTime * (destination - enemy.transform.position).normalized;
        }
        else
        {
            processor.CoolDownState.OnStateEnter();
            processor.currentState = processor.CoolDownState;
        }
        buffer += Time.deltaTime;
    }
}
public class CoolDown_State : ChargerState
{
    float buffer = 0.0f;
    public CoolDown_State(ChargerStateProcessor context, ChargerAI enemy) : base(context, enemy){}
    public override void Update()
    {
        Debug.Log("CoolDown");
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
