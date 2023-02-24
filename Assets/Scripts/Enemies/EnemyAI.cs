using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHittable
{
    [Header("EnemyData:")]
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] public EnemyData enemyData;

    public NavMeshAgent agent;
    public Transform target;
    public Animator animator;
    private EnemySword sword;
    private Rigidbody rb;

    [Header("Movement parameters:")]
    private float distance;
    public float Distance 
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }    
    }
    public bool canMove = true;

    //State processor to handle states flow
    private EnemyStateProcessor stateProcessor;


    private void Awake()
    {
        //Enemy data init and sowrd init
        enemyData = new EnemyData(enemyDataSO);
        sword = GetComponentInChildren<EnemySword>();
        sword.Init(enemyData);



        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        
        rb = GetComponentInChildren<Rigidbody>();
        stateProcessor = new EnemyStateProcessor(this, sword);
        stateProcessor.Init();
    }

    void Update()
    {
        if (canMove == true)
        {
            stateProcessor.Update();
        }
    }

    public void OnHit(Vector3 knockBackDir, SwordData sword)
    {
        //Logic to init anche change state in state processor
        stateProcessor.knockBackState.Init(knockBackDir, sword.knockBackSpeed, sword.knockBackDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.knockBackState;   
    }
}
