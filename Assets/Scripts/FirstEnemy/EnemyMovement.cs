using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private Transform player;

    private NavMeshAgent agent;

    public float lookRadius = 20f;

    private bool isUp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

       if (distance <= lookRadius && distance > 4f)
        {
            agent.SetDestination(player.position);
            Debug.Log(Vector3.Distance(transform.position, player.position));
        }
        else if(distance < 4)
        {
            agent.velocity = Vector3.zero;
        }
    }

    private void MoveToPlayer()
    {
        
            
        
    }
}
