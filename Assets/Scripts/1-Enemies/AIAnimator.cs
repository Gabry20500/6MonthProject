using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AIAnimator : EAnimator
{    
    private NavMeshAgent agent;
    private SpriteRenderer _renderer;
    private Direction lastChargeDir;
    private Vector2 dir;
    private Vector2 last;
    public bool charging = false;
    public bool dashing = false;
    private void Awake()
    {
        lastDir = Direction.S;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (dashing == false)
        {
            if (charging == true)
            {
                _animator.Play("Charging");


                return;
            }
            dir = new Vector2(agent.velocity.x, agent.velocity.z);
            if (dir.magnitude <= 0.1f)
            {
                SetDirection(Vector2.down, true);
            }
            else
            {
                last = dir;
                SetDirection(dir);
            }
        }
    }

    public IEnumerator Charging_Color(float duration)
    {
        float buffer = 0.0f;
        while(buffer < duration)
        {
            _renderer.color = new Color(1, 1 - (buffer / duration), 1 - (buffer / duration));
            buffer += Time.deltaTime;
            yield return null;
        }
        _renderer.color = new Color(1, 1, 1);
    }

    private void SetDashAnimation()
    {
        
    }
}
