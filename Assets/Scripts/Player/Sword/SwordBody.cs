using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBody : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ok");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            collision.gameObject.GetComponentInChildren<Enemy>().SetCurrentHp(_player.Attack);
        }
    }
}
