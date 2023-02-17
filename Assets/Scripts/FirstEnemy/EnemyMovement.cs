using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private Transform _player;
    private NavMeshAgent _agent;
    private EnemyAttack _enemyAttack;
    private Animator _animator;

    [Header("Enemy movement")]
    [SerializeField] private float lookRadius = 20f;
    [SerializeField] private float minValueToAttack = 4f;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.position, transform.position);

       if (distance <= lookRadius && distance > minValueToAttack)
        {
            _agent.SetDestination(_player.position);
            _animator.SetBool("Attack", false);
        }
        else
        {
            _agent.velocity = Vector3.zero;
            _animator.SetBool("Attack", true);
            _enemyAttack.Attack(1);
        }
    }
}
