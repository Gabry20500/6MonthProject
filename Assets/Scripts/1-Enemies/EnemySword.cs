using UnityEngine;
using System.Collections;

public class EnemySword : MonoBehaviour
{
    [Header("Sword data:")] 
    [SerializeField] public EnemyData owner_En;

    private bool isAttacking = false;
    public bool IsAttacking { set => isAttacking = value; }

    [SerializeField] AudioClip baseSwing;
    [SerializeField] AudioClip baseClash;
    AudioSource enemyS_Audio;

    private Vector3 knockDir;
    private EnemyAI my_En_AI;
    private void Awake()
    {
        enemyS_Audio = GetComponent<AudioSource>();
    }

    public void Init(EnemyData enemyData)
    {
        owner_En = enemyData;
        my_En_AI = gameObject.GetComponentInParent<EnemyAI>();
    }

    bool canDamage = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword") && collision.gameObject.GetComponent<Sword>().canRotate == false)
        {
            //Passing knock back direction to applicate to te hitted entity
            knockDir = Utils.CalculateDir(transform.parent.position, collision.gameObject.transform.position);
            PlayAudio(baseClash);
            my_En_AI.OnClash(knockDir, collision.gameObject.GetComponent<Sword>().swordData);
        }

        else if (collision.gameObject.CompareTag("Player") && isAttacking == true && canDamage == true)
        {
            canDamage = false;
            knockDir = Utils.CalculateDir(collision.gameObject.transform.position, transform.parent.position);
            collision.gameObject.GetComponent<EMovement>().OnHit(knockDir, owner_En, owner_En.swordDamage);
            StartCoroutine(Utils.FreezeFrames(owner_En.freeze_Intensity, owner_En.freeze_Duration));
            StartCoroutine(PlayerTickDamage(collision)); 
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        enemyS_Audio.clip = clip;
        enemyS_Audio.Play();
    }
    private IEnumerator PlayerTickDamage(Collision collision)
    {
        PlayAudio(baseSwing);
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
