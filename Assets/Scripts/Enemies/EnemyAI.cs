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
    private EnemySword enemySword;

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
        enemySword = GetComponentInChildren<EnemySword>();
        enemySword.Init(enemyData);

        //Find player in the world and get self NavMeshAgent and animator
        target = GameObject.FindWithTag("Player").transform;     
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        //animator = GetComponentInChildren<Animator>();
        
        //Initialize state processor and states
        stateProcessor = new EnemyStateProcessor(this, enemySword);
        stateProcessor.Init();
    }

    //Every frame update current state in stateprocessor
    void Update()
    {
        if (canMove == true)
        {
            stateProcessor.Update();
        }
    }

    public void OnHit(float damage, Vector3 knockDir, SwordData sword)
    {
        GetComponent<Entity>().TakeDamage(damage);
        //Logic to init anche change state in state processor
        stateProcessor.knockBackState.Init(knockDir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.knockBackState;   
    }

    public void OnClash(Vector3 knockBackDir, SwordData sword)
    {
        stateProcessor.knockBackState.Init(knockBackDir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.knockBackState;
    }
}
