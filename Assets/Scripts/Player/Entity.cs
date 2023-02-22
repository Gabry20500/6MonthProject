using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private Slider healtBar;
    [SerializeField] private float health = 30;
    [SerializeField] private float maxHealth = 30;

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

    private void Awake()
    {
        healtBar.maxValue = maxHealth;
        healtBar.value = health;
    }

    public void TakeDamage(float value)
    {
        health -= value;
        healtBar.value = health;
        if(health <= 0.0f)
        {
            GetComponent<EnemyAI>().Die();
        }
    }
}
