using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [Header("Sword data:")] 
    [SerializeField] public float swordDamage;

    private bool isAttacking = false;
    public bool IsAttacking { set => isAttacking = value; }

    public void Init(EnemyData enemyData)
    {
        swordDamage = enemyData.swordDamage;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAttacking == true)
        {
            //Here must pass the position of the entity that swing, so posso fare la direzione

            collision.gameObject.GetComponentInParent<Entity>().TakeDamage(swordDamage, collision);
        }
    }
}
