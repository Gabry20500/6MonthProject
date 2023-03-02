public class EnemyStateProcessor
{
    public EnemyAI enemy;

    public IdleState idleState;
    public SeekState seekState;
    public AttackState attackState;
    public KnockBackState knockBackState;

    public EnemyState currentState;

    

    public EnemyStateProcessor(EnemyAI context, EnemySword sword)
    {
        enemy = context;
        idleState = new IdleState(this, enemy);
        seekState = new SeekState(this, enemy);
        attackState = new AttackState(this, enemy, sword);
        knockBackState = new KnockBackState(this, enemy);
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
