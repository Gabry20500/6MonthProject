using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [Header("Sword data:")] 
    [SerializeField] public EnemyData enemyData;

    private bool isAttacking = false;
    public bool IsAttacking { set => isAttacking = value; }

    [SerializeField] AudioClip baseSwingEffect;
    [SerializeField] AudioClip baseClashEffect;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(EnemyData enemyData)
    {
        this.enemyData = enemyData;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword") && collision.gameObject.GetComponent<Sword>().canRotate == false)
        {
            //Passing knock back direction to applicate to te hitted entity
            Vector3 knockBackDir = transform.parent.position - collision.gameObject.transform.position;
            knockBackDir = new Vector3(knockBackDir.x, 0.0f, knockBackDir.z);
            knockBackDir.Normalize();

            audioSource.clip = baseClashEffect;
            audioSource.Play();

            this.gameObject.GetComponentInParent<EnemyAI>().OnClash(knockBackDir, collision.gameObject.GetComponent<Sword>().sword);

        }

        else if (collision.gameObject.CompareTag("Player") && isAttacking == true)
        {
            Vector3 knockBackDir = collision.gameObject.transform.position - transform.parent.position;
            knockBackDir = new Vector3(knockBackDir.x, 0.0f, knockBackDir.z);
            knockBackDir.Normalize();


            audioSource.clip = baseSwingEffect;
            audioSource.Play();

            collision.gameObject.GetComponent<EMovement>().OnHit(enemyData.swordDamage, knockBackDir, enemyData);
        }
    }
}
