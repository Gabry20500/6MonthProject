using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob_Spawner : MonoBehaviour
{
    public int enemyNum;
    [SerializeField] private List<SpawnPointEnemy> enemiesSpawnPoint;
    [SerializeField] private List<SpawnPointEnemy> occupiedSpawnPoint;
    [SerializeField] private List<Collider> _doors;
    [SerializeField] private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, enemiesSpawnPoint.Count);
        for (int i = 0; i < enemyNum; i++)
        {
            GameObject enemy = Instantiate(EnemyPool.instance.baseEnemy, enemiesSpawnPoint[rand].transform.position,Quaternion.identity,enemyParent.transform);
            enemy.GetComponent<Enemy>().MyRoom = this;
            enemiesSpawnPoint[rand].isOccupied = true;
            occupiedSpawnPoint.Add(enemiesSpawnPoint[rand]);
            enemiesSpawnPoint.Remove(enemiesSpawnPoint[rand]);
            rand = Random.Range(0, enemiesSpawnPoint.Count);
        }
        
        foreach (var door in _doors)
        {
            door.isTrigger = false;
        }
    }

    private void Update()
    {
        Debug.Log(enemyNum);
    }

    public void OpenDoors()
    {
        foreach (var door in _doors)
        {
            door.isTrigger = true;
        }   
    }
    
    public void EnemyDeath()
    {
        if (enemyNum > 0)
        {
            enemyNum--;
        }
        
        if (enemyNum <= 0)
        {
            OpenDoors();
        }
    }
}
