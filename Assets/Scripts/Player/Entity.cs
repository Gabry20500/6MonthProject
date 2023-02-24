using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Slider healtBar;
    [SerializeField] protected float health = 30;
    [SerializeField] protected float maxHealth = 30;

    [SerializeField] protected AudioClip hitClip;
    protected AudioSource audioSource;
    #region Getter
    public float Healt
    {
        get { return health; }
        set {  health = value; }
    }

    
    public float MaxHealth
    {
        get { return maxHealth; }
        set {  maxHealth = value; }
    }

    #endregion

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hitClip;
        healtBar.maxValue = maxHealth;
        healtBar.value = health;
    }

    public void TakeDamage(float value, SwordData otherSword, Vector3 knockDir, EnemyData enemy = null)
    {
        audioSource.Play();
        health -= value;
        healtBar.value = health;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
        ReceiveKnockBack(otherSword, knockDir, enemy);

    }

    public void ReceiveKnockBack(SwordData otherSword, Vector3 knockDir, EnemyData enemy)
    {
        if (gameObject.tag == "Player")
        {
            EMovement AI = GetComponent<EMovement>();
            if (AI == null) { return; }
            AI.OnHit(knockDir, enemy);
        }
        else
        {
            EnemyAI AI = GetComponent<EnemyAI>();
            if (AI == null) { return; }
            AI.OnHit(knockDir, otherSword);
        }
    }
}
