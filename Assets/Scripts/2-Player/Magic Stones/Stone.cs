using UnityEngine;

public enum StoneElement
{
    NONE,
    FIRE,
    WATER,
    AIR,
    EARTH
}

[CreateAssetMenu(fileName = "BseStone", menuName = "Stone/New BaseStone", order = 0)]
public class Stone : ScriptableObject
{
    #region Variables
    [SerializeField] private StoneElement element = StoneElement.NONE;
    [SerializeField] private GameObject stoneVFX = null;
    [SerializeField] private Sprite uiImage = null;

    private GameObject instance = null;
    private TrailRenderer normalTrail = null;
    #endregion

    #region Getter
        public StoneElement Element
        {
             get { return element; }
        }
        public Sprite Image
        {
            get { return uiImage; }
        }
    #endregion

    public virtual void OnPickedUp(Player player) 
    {
        player.PickUp_Stone(this);
    }
    public virtual void OnDiscarded(Player player) 
    {
        player.Discard_Stone(this);
    }
    public virtual void OnSelected(Sword sword) 
    {
        instance = Instantiate(stoneVFX, sword.transform.position, Quaternion.identity, sword.transform);
        normalTrail = sword.gameObject.GetComponentInChildren<TrailRenderer>();
        normalTrail.enabled = false;
        sword.trail = instance.GetComponentInChildren<TrailRenderer>();
    }
    public virtual void OnDeselected(Sword sword) 
    {
        if (instance != null) DestroyImmediate(instance);
        sword.trail = normalTrail;
        sword.trail.enabled = true;
    }


    public virtual void OnEnemyHitted(Sword sword, EnemyAI enemy)
    {
            sword.AddMana();
    }
}