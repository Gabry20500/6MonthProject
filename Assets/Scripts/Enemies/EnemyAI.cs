using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Transform targetPlayer;
    public Animator _animator;

    [Header("Movement parameters:")]
    [SerializeField] public float sightDistance = 20f;
    [SerializeField] public float attackReach = 4f;
    public float distance;

    [Header("Attack parameters")]
    [SerializeField] public float attackRate = 0.20f;
    [SerializeField] public float attackDamage = 10f;

    private EnemyStateProcessor stateProcessor;

    private void Awake()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        stateProcessor = new EnemyStateProcessor(this);
    }

    private void Start()
    {
        stateProcessor.Init();
    }
    void Update()
    {
        stateProcessor.Update();
    }

    public void Die()
    {
        Destroy(gameObject.transform.parent);
    }
}
