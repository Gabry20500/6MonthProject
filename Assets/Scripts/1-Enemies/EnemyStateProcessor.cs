public class EnemyStateProcessor
{
    public EnemyAI enemy;

    public IdleState idleState;
    public SeekState seekState;
    public AttackState attackState;
    public KnockState knockBackState;
    public StunState stunState;

    public EnemyState currentState;

    

    public EnemyStateProcessor(EnemyAI context, EnemySword sword)
    {
        enemy = context;
        idleState = new IdleState(this, enemy);
        seekState = new SeekState(this, enemy, sword);
        attackState = new AttackState(this, enemy, sword);
        knockBackState = new KnockState(this, enemy);
        stunState = new StunState(this, enemy);
    }

    public void Init()
    {
        currentState = idleState;
    }

    public void Update()
    {
        currentState.Update();
    }
}
