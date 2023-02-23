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

    public void TakeDamage(float value, Collision collision)
    {
        audioSource.Play();     
        health -= value;
        healtBar.value = health;
        if(health <= 0.0f)
        {
            Destroy(gameObject);
        }
        if (gameObject.tag == "Player")
        {
            EntityMovement player = GetComponent<EntityMovement>();
            //player.OnHit(collision);
        }
        else
        {
            EnemyAI enemy = GetComponent<EnemyAI>();
            //enemy.OnHit(collision);
        }

    }
}
