using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Entity : MonoBehaviour
{
    [SerializeField] protected Slider healtBar;
    [SerializeField] protected float health = 30;
    [SerializeField] protected float maxHealth = 30;

    [SerializeField] protected AudioClip hitClip;
    protected AudioSource eAudio;
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
        eAudio = GetComponent<AudioSource>();
        InitHealthBar();
    }

    private void InitHealthBar()
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
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        eAudio.clip = clip;
        eAudio.Play();
    }
}
