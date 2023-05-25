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
    [SerializeField] private Sprite uiImage = null;
    [SerializeField] private Mesh swMesh = null;
    [SerializeField] private Material swMaterial = null;
    [SerializeField] private Material trailMaterial = null;
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
        public Mesh Mesh
        {
            get { return swMesh; }
        }
        public Material SwMaterial
        {
            get { return swMaterial; }
        }
        public Material TrailMaterial
        {
            get { return trailMaterial; }
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
        sword.gameObject.GetComponent<MeshFilter>().mesh = swMesh;
        sword.gameObject.GetComponent<MeshRenderer>().materials[0] = swMaterial;
        sword.gameObject.GetComponentInChildren<TrailRenderer>().materials[0] = trailMaterial;
    }
    public virtual void OnDeselected(Sword sword) { }
    public virtual void OnEnemyHitted(Sword sword, EnemyAI enemy) { }
}