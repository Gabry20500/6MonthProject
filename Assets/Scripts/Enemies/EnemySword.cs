using UnityEngine;
using System.Collections;

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

    bool canDamage = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword") && collision.gameObject.GetComponent<Sword>().canRotate == false)
        {
            //Passing knock back direction to applicate to te hitted entity
            Vector3 knockDir = CalculateDir(transform.parent.position, collision.gameObject.transform.position);

            PlayAudio(baseClash);

            gameObject.GetComponentInParent<EnemyAI>().OnClash(knockDir, collision.gameObject.GetComponent<Sword>().swordData);
        }

        else if (collision.gameObject.CompareTag("Player") && isAttacking == true && canDamage == true)
        {
            canDamage = false;
            StartCoroutine(PlayerTickDamage(collision));
            
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        enemyS_Audio.clip = clip;
        enemyS_Audio.Play();
    }

    private Vector3 CalculateDir(Vector3 A, Vector3 B)
    {
        Vector3 dir = A - B;
        dir = new Vector3(dir.x, 0.0f, dir.z);
        return dir.normalized;
    }

    private IEnumerator PlayerTickDamage(Collision collision)
    {
        Vector3 knockdir = CalculateDir(collision.gameObject.transform.position, transform.parent.position);

        PlayAudio(baseSwing);

        collision.gameObject.GetComponent<EMovement>().OnHit(ownerEnemy.swordDamage, knockdir, ownerEnemy);
        float t = 0;
        float tickTime = 1f;
        while(t < tickTime)
        {
            yield return null;
            t += Time.deltaTime;
        }
        canDamage = true;

    }
}
