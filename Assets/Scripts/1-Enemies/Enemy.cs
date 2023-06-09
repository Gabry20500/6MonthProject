public class Enemy : Entity
{
    protected Mob_Spawner my_Room;

    public Mob_Spawner MyRoom
    {
        get { return my_Room; }
        set { my_Room = value; }
    }

    protected override void Death()
    {
        my_Room.EnemyDeath();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void InitParameters(EnemyData data)
    {
        HP = data.HP;
        max_HP = data.max_HP;
        InitHealthBar();
    }
}
