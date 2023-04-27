using UnityEngine;
using UnityEngine.AI;

public class ChargerAI : EnemyAI
{
    [SerializeField] private ChargerDataSO ch_enemy_SO;
    [SerializeField] public  ChargerData chenemy_Data;
    protected ChargerStateProcessor stateProcessor;

    public new float Distance
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
    public new EnemyData Enemy_Data
    {
        get { return chenemy_Data; }
    }

    protected new void Awake()
    {
        //Find player in the world and get self NavMeshAgent and animator
        target = FindObjectOfType<Player>().transform;
        nav_Agent = GetComponent<NavMeshAgent>();
        nav_Agent.updateRotation = false;


        //Enemy data init and sowrd init
        chenemy_Data = new ChargerData(ch_enemy_SO);
        enemy_Sword = GetComponentInChildren<EnemySword>();
        enemy_Sword.Init(chenemy_Data);
        enemy = GetComponent<Enemy>();
        enemy.InitParameters(chenemy_Data);
    }
    private void Start()
    {
        //Initialize state processor and states
        stateProcessor = new ChargerStateProcessor(this, enemy_Sword);
        stateProcessor.Init();
    }

    void Update()
    {
        if (canMove == true)
        {
            stateProcessor.Update();
        }
    }
}
