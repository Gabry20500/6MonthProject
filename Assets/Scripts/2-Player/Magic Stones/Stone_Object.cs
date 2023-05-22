using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Object : MonoBehaviour
{
    [SerializeField] Stone stone;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Sword sw = collision.gameObject.GetComponent<EMovement>().Sword;
            bool flag = sw.PickUp_Stone(stone);
            if(flag == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
