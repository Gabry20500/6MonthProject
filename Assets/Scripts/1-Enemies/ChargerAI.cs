using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ChargerAI : EnemyAI
{
    private bool canHit = true;
    private bool canBeDamaged = true;

    [SerializeField] public GameObject pointer;
    [SerializeField] public AudioClip dashSound;

    [SerializeField] private ChargerDataSO ch_enemy_SO;
    [SerializeField] public  ChargerData chenemy_Data;
    protected ChargerStateProcessor stateProcessor;

    public ChargerAnimator ai_Animator;
    private bool isAttacking = false;

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
    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
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

        ai_Animator = GetComponentInChildren<ChargerAnimator>();
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
    private void OnCollisionEnter(Collision collision)
    {
        //if (canHit == true)
        //{
            
            if (collision.gameObject.CompareTag("Player") && isAttacking == true)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(2.0f);
            }
            //canHit = false;
            //StartCoroutine(Charger_Hit_Cooldown(1.0f));
        //}
    }

    public override void OnHit(float damage, Vector3 knock_Dir, Player_SwordData sword)
    {
        if (canBeDamaged == true)
        {
            enemy.TakeDamage(damage);
            //Logic to init anche change state in state processor
            //stateProcessor.KnockBackState.Init(knock_Dir, sword.knockSpeed, sword.knockDuration);
            //stateProcessor.currentState.OnStateExit();
            //stateProcessor.currentState = stateProcessor.KnockBackState;
        }
    }

    private IEnumerator Charger_Hit_Cooldown(float coolTime)
    {
        float buffer = 0.0f;
        while(buffer < coolTime)
        {
            buffer += Time.deltaTime;
            yield return null;
        }
        canHit = true;
    }
}
