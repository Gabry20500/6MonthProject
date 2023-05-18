using UnityEngine;

public enum StoneElement
{
    None,
    Fire,
    Water,
    Air,
    Earth
}

[CreateAssetMenu(fileName = "MagicStone", menuName = "New Stone", order = 0)]
public class Stone : ScriptableObject
{
    #region Variables

    [SerializeField] private StoneElement stoneElement;
    [SerializeField] private Sprite uiImage;
    [SerializeField] private Mesh gameImage;
    [SerializeField] private bool damageInTime;
    [SerializeField] private bool slowDown;
    [SerializeField] private bool speedUp;

    #endregion

    #region Getter

    public StoneElement GetStoneElement()
    {
        return stoneElement;
    }

    public Sprite GetUiImage()
    {
        return uiImage;
    }

    public Mesh GetGameMesh()
    {
        return gameImage;
    }

    public bool IsDamageInTime()
    {
        return damageInTime;
    }

    public bool IsSlowDown()
    {
        return slowDown;
    }

    public bool IsSpeedUp()
    {
        return speedUp;
    }

    #endregion
}