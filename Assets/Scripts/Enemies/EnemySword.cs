using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [Header("Sword data:")] 
    [SerializeField] public EnemyData enemyData;

    private bool isAttacking = false;
    public bool IsAttacking { set => isAttacking = value; }

    public void Init(EnemyData enemyData)
    {
        this.enemyData = enemyData;
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && isAttacking == true)
    //    {
    //        //Passing knock back direction to applicate to te hitted entity
    //        Vector3 knockBackDir = collision.gameObject.transform.position - transform.parent.position;
    //        knockBackDir = new Vector3(knockBackDir.x, 0.0f, knockBackDir.z);
    //        knockBackDir.Normalize();

    //        collision.gameObject.GetComponent<Entity>().TakeDamage(enemyData.swordDamage, null, knockBackDir, enemyData);
    //    }

    //    //If colliding a player sword simply knock the player
    //    //cALL RECEIVEKNOCKBACK IN The entity scrpt
    //}


    //problema spada vola via
}
