using UnityEngine;
using UnityEngine.UI;
public class Entity : MonoBehaviour
{
    [SerializeField] protected Slider healtBar;
    [SerializeField] protected float health = 30;
    [SerializeField] protected float maxHealth = 30;

    [SerializeField] protected AudioClip hitClip;
    protected AudioSource entity_Audio;
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
        entity_Audio = GetComponent<AudioSource>();
        InitHealthBar();
    }

    protected virtual void InitHealthBar()
    {
        healtBar.maxValue = maxHealth;
        healtBar.value = health;
    }

    public void TakeDamage(float value)
    {
        if (value != 0.0f)
        {
            PlaySound(hitClip);
        }
        health -= value;
        healtBar.value = health;
        if (health <= 0.0f)
        {
            Death();
        }
    }

    protected virtual void PlaySound(AudioClip clip)
    {
        entity_Audio.clip = clip;
        entity_Audio.Play();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
