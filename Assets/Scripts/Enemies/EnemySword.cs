using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private bool isAttacking = false;

    public bool IsAttacking { set => isAttacking = value; }

    private void OnEnable()
    {
        damage = GetComponentInParent<EnemyAI>().attackDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAttacking == true)
        {
            Debug.Log("Player");
            //Here must pass the position of the entity that swing, so posso fare la direzione
            collision.gameObject.GetComponentInParent<Entity>().TakeDamage(damage, collision);
        }
    }
}
