using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireStone : MonoBehaviour, IMagicStone
{

    #region Variables

    [SerializeField] private Stone scriptableObject;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = scriptableObject.GetGameMesh();
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
        
    }

    public void AddEffect()
    {
        
    }
}
