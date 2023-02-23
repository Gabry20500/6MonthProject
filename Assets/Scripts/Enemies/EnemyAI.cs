using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Transform targetPlayer;
    public Animator _animator;
    private EnemySword sword;
    private Rigidbody _rigidBody;

    [Header("Movement parameters:")]
    [SerializeField] public float sightDistance = 20f;
    [SerializeField] public float attackReach = 4f;
    public float distance;

    [Header("Attack parameters")]
    [SerializeField] public float attackRate = 0.20f;
    [SerializeField] public float attackDamage = 10f;

    private EnemyStateProcessor stateProcessor;

    public bool canMove = true;

    private void Awake()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        sword = GetComponentInChildren<EnemySword>();
        _rigidBody = GetComponentInChildren<Rigidbody>();
        stateProcessor = new EnemyStateProcessor(this, sword);
    }

    private void Start()
    {
        stateProcessor.Init();
    }
    void Update()
    {
        if (canMove == true)
        {
            stateProcessor.Update();
        }
    }

    public void OnHit(Collision collision)
    {
        Debug.Log("Entering INTERFACE ENEMY");
        //Logic to init anche change state in state processor
        //logic to detect collision direction normal
    }

}
