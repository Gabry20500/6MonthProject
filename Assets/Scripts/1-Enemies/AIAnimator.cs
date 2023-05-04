using UnityEngine;
using UnityEngine.AI;

public class AIAnimator : EAnimator
{    
    private NavMeshAgent agent;
    private Vector2 dir;
    private Vector2 last;
    private bool charging = false;
    private void Awake()
    {
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(charging == true)
        { return; }
        dir = new Vector2(agent.velocity.x, agent.velocity.z);
        if(dir.magnitude <= 0.1f)
        {
            SetDirection(Vector2.down, true);
        }
        else
        {
            last = dir;
            SetDirection(dir);
        }
    }

    public void ChargingAnimation()
    {
        charging = true;
        _animator.Play("Charging");
    }

    public void DeCharge()
    {
        charging = false;
    }
}
