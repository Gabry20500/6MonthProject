using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneUIManager : MonoBehaviour
{

    public List<Image> manaUsable;
    public List<Image> usedMana;

    public void UseManaCristal()
    {
        if(manaUsable.Count > 0)
        {
            usedMana.Add(manaUsable[0]);
            manaUsable.Remove(manaUsable[0]);
        }
        else
        {
            Debug.LogError("No mana");
        }
    }

    public void AddManaCristal()
    {
        if (usedMana.Count > 0)
        {
            manaUsable.Add(usedMana[0]);
            usedMana.Remove(usedMana[0]); 
        }
        else
        {
            Debug.LogError("No more mana");
        }
    }
}
