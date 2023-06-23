using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class AIAnimator : EAnimator
{    
    protected NavMeshAgent agent;
    

    protected Vector2 dir;
    protected Vector2 last;

    protected void Awake()
    {
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        dir = new Vector2(agent.velocity.x, agent.velocity.z);
        if (dir.magnitude < 0.1f)
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
