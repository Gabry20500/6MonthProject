using UnityEngine;
using UnityEngine.AI;

public class AIAnimator : EAnimator
{    
    private NavMeshAgent agent;
    private Vector2 dir;
    private Vector2 last;
    private void Awake()
    {
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        dir = new Vector2(agent.velocity.x, agent.velocity.z);
        if(dir.magnitude <= 0.1f)
        {
            SetDirection(last, true);
        }
        else
        {
            last = dir;
            SetDirection(dir);
        }
    }
}
