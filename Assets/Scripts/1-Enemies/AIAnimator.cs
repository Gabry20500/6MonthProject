using UnityEngine;
using UnityEngine.AI;

public class AIAnimator : EAnimator
{    
    private NavMeshAgent agent;
    Vector2 dir;
    private void Awake()
    {
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        dir = new Vector2(agent.velocity.x, agent.velocity.z);
        SetDirection(dir);
    }
}
