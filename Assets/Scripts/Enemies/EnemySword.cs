using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [Header("Sword data:")] 
    [SerializeField] public EnemyData ownerEnemy;

    private bool isAttacking = false;
    public bool IsAttacking { set => isAttacking = value; }

    [SerializeField] AudioClip baseSwing;
    [SerializeField] AudioClip baseClash;
    AudioSource enemyS_Audio;
    private void Awake()
    {
        enemyS_Audio = GetComponent<AudioSource>();
    }

    public void Init(EnemyData enemyData)
    {
        ownerEnemy = enemyData;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword") && collision.gameObject.GetComponent<Sword>().canRotate == false)
        {
            //Passing knock back direction to applicate to te hitted entity
            Vector3 knockDir = transform.parent.position - collision.gameObject.transform.position;
            knockDir = new Vector3(knockDir.x, 0.0f, knockDir.z);
            knockDir.Normalize();

            enemyS_Audio.clip = baseClash;
            enemyS_Audio.Play();

            gameObject.GetComponentInParent<EnemyAI>().OnClash(knockDir, collision.gameObject.GetComponent<Sword>().swordData);
        }

        else if (collision.gameObject.CompareTag("Player") && isAttacking == true)
        {
            Vector3 knockDir = collision.gameObject.transform.position - transform.parent.position;
            knockDir = new Vector3(knockDir.x, 0.0f, knockDir.z);
            knockDir.Normalize();


            enemyS_Audio.clip = baseSwing;
            enemyS_Audio.Play();

            collision.gameObject.GetComponent<EMovement>().OnHit(ownerEnemy.swordDamage, knockDir, ownerEnemy);
        }
    }
}
