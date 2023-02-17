using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;
    
    private void Awake()
    {
        GameObject plyGo = GameObject.FindGameObjectWithTag("Player");
        _player = plyGo.GetComponent<Player>();
        _enemy = GetComponentInChildren<Enemy>();
    }

    public void Attack(int damage)
    {
        _player.SetCurrentHp(_enemy.Attack);
    }
}
