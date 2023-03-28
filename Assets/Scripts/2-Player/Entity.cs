using UnityEngine;
using UnityEngine.UI;
public class Entity : MonoBehaviour
{
    [SerializeField] protected Slider HP_Bar;
    [SerializeField] protected float HP = 30;
    [SerializeField] protected float max_HP = 30;

    [SerializeField] protected AudioClip hit_Clip;
    protected AudioSource entity_Audio;

    #region Getter
    public float Healt
    {
        get { return HP; }
        set {  HP = value; }
    }

    public float MaxHealth
    {
        get { return max_HP; }
        set {  max_HP = value; }
    }
    public AudioClip Hit_Clip
    {
        get { return hit_Clip; }
        set { hit_Clip = value; }
    }
    #endregion

    protected virtual void Awake()
    {
        entity_Audio = GetComponent<AudioSource>();
    }

    protected virtual void InitHealthBar()
    {
        HP_Bar.maxValue = max_HP;
        HP_Bar.value = HP;
    }

    public virtual void TakeDamage(float value)
    {
        if (value != 0.0f)
        {
            PlaySound(hit_Clip);
        }
        HP -= value;
        HP_Bar.value = HP;
        if (HP <= 0.0f)
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

    }
}
