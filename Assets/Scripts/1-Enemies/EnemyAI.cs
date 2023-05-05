using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHittable, IClashable
{
    [Header("EnemyData:")]
    [SerializeField] private EnemyDataSO enemy_SO;
    private EnemyData enemy_Data;
    protected Enemy enemy;

    public NavMeshAgent nav_Agent;
    public Transform target = null;
    public Animator enemy_Animator;
    protected EnemySword enemy_Sword;

    [Header("Movement parameters:")]
    protected float target_Distance;
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

    public EnemyData Enemy_Data
    {
       get { return enemy_Data; }
    }
    protected void Awake()
    {
        //Enemy data init and sowrd init
        enemy_Data = new EnemyData(enemy_SO);
        enemy_Sword = GetComponentInChildren<EnemySword>();
        enemy_Sword.Init(enemy_Data);
        enemy = GetComponent<Enemy>();
        enemy.InitParameters(enemy_Data);

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

    public virtual void OnHit(float damage, Vector3 knock_Dir, Player_SwordData sword)
    {
        enemy.TakeDamage(damage);
        //Logic to init anche change state in state processor
        stateProcessor.KnockBackState.Init(knock_Dir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.KnockBackState;   
    }

    public void OnClash(Vector3 knockBackDir, Player_SwordData sword)
    {
        stateProcessor.KnockBackState.Init(knockBackDir, sword.knockSpeed, sword.knockDuration);
        stateProcessor.currentState.OnStateExit();
        stateProcessor.currentState = stateProcessor.KnockBackState;
    }
}
