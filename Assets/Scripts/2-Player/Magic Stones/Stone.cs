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
        if (stoneVFX != null)
        {
            StoneUtils.instance = Instantiate(stoneVFX, sword.transform.position, sword.transform.rotation, sword.transform)as GameObject;
            StoneUtils.normalTrail = sword.gameObject.GetComponentInChildren<TrailRenderer>();
            StoneUtils.normalTrail.enabled = false;
            sword.trail = StoneUtils.instance.GetComponentInChildren<TrailRenderer>();
        }
        
    }
    public virtual void OnDeselected(Sword sword) 
    {
        if (StoneUtils.instance != null)
        {
            Destroy(StoneUtils.instance);
            sword.trail = StoneUtils.normalTrail;
            sword.trail.enabled = true;
        }
    }


    public virtual void OnEnemyHitted(Sword sword, EnemyAI enemy)
    {
            sword.AddMana();
    }
}

static class StoneUtils
{
    public static GameObject instance = null;
    public static TrailRenderer normalTrail = null;
}