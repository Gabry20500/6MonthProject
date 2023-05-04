public class EnemyStateProcessor
{
    public EnemyAI enemy;

    private IdleState idleState;
    private SeekState seekState;
    private AttackState attackState;
    protected KnockState knockBackState;
    protected StunState stunState;
    public State currentState;

    #region Getter
    public IdleState IdleState
    {
        get { return idleState; }
    }
    public SeekState SeekState
    {
        get { return seekState; }
    }
    public AttackState AttackState
    {
        get { return attackState; }
    }
    public KnockState KnockBackState
    {
        get { return knockBackState; }
    }
    public StunState StunState
    {
        get { return stunState; }
    }
    #endregion

    public EnemyStateProcessor(EnemyAI context, EnemySword sword)
    {
        enemy = context;
        idleState = new IdleState(this, enemy);
        seekState = new SeekState(this, enemy, sword);
        attackState = new AttackState(this, enemy, sword);
        knockBackState = new KnockState(this, enemy);
        stunState = new StunState(this, enemy);
    }
    public virtual void Init()
    {
        currentState = idleState;       
    }
    public virtual void Update()
    {
        currentState.Update();
    }
}
public class ChargerStateProcessor : EnemyStateProcessor
{
    public new ChargerAI enemy;

    private Charger_Idle_State idleState;
    private Charger_SeekState chseekState;
    private Charger_Charging_State chargingState;
    private Dash_State dashState;
    private CoolDown_State coolDownState;


    #region Getter
    public new Charger_Idle_State IdleState
    {
        get { return idleState; }
    }
    public Charger_SeekState ChseekState
    {
        get { return chseekState; }
    }

    public Charger_Charging_State ChargingState
    {
        get { return chargingState; }
    }
    public Dash_State DashState
    {
        get { return dashState; }
    }

    public CoolDown_State CoolDownState
    {
        get { return coolDownState; }
    }
    #endregion

    public ChargerStateProcessor(ChargerAI context, EnemySword sword) : base(context, sword)
    {
        enemy = context;
        idleState = new Charger_Idle_State(this, enemy);
        chseekState = new Charger_SeekState(this, enemy, sword);
        chargingState = new Charger_Charging_State(this, enemy);
        knockBackState = new KnockState(this, enemy);
        stunState = new StunState(this, enemy);
        dashState = new Dash_State(this, enemy);
        coolDownState = new CoolDown_State(this, enemy);
    }
    public override void Init()
    {
        currentState = idleState;
        currentState.OnStateEnter();
    }
    public override void Update()
    {
        currentState.Update();
    }
}
