using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHittable, IClashable
{
    [Header("EnemyData:")]
    [SerializeField] private EnemyDataSO enemy_SO;
    [SerializeField] public EnemyData enemy_Data;
    private Enemy enemy;

    public NavMeshAgent nav_Agent;
    public Transform target;
    public Animator enemy_Animator;
    private EnemySword enemy_Sword;

    [Header("Movement parameters:")]
    private float target_Distance;
    public float Distance 
    {
        get
        {
            return target_Distance;
        }
        set
        {
            target_Distance = value;
        }    
    }
    public bool canMove = true;

    //State processor to handle states flow
    private EnemyStateProcessor stateProcessor;


    private void Awake()
    {
        //Enemy data init and sowrd init
        enemy_Data = new EnemyData(enemy_SO);
        enemy_Sword = GetComponentInChildren<EnemySword>();
        enemy_Sword.Init(enemy_Data);
        enemy = GetComponent<Enemy>();

        //Find player in the world and get self NavMeshAgent and animator
        target = GameObject.FindWithTag("Player").transform;     
        nav_Agent = GetComponent<NavMeshAgent>();
        nav_Agent.updateRotation = false;

        //animator = GetComponentInChildren<Animator>();
        
        //Initialize state processor and states
        stateProcessor = new EnemyStateProcessor(this, enemy_Sword);
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

    public void OnHit(float damage, Vector3 knock_Dir, Player_SwordData sword)
    {
        enemy.TakeDamage(damage);
        //Logic to init anche change state in state processor
        stateProcessor.knockBackState.Init(knock_Dir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.knockBackState;   
    }

    public void OnClash(Vector3 knockBackDir, Player_SwordData sword)
    {
        stateProcessor.knockBackState.Init(knockBackDir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.knockBackState;
    }
}
