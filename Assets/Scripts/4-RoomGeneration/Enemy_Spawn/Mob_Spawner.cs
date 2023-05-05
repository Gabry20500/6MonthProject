using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mob_Spawner : MonoBehaviour
{
    public int enemyNum;
    [SerializeField] private List<SpawnPointEnemy> enemiesSpawnPoint;
    [SerializeField] private List<SpawnPointEnemy> occupiedSpawnPoint;
    [SerializeField] private List<DoorManager> _doors;
    [SerializeField] private int enemys;
    [SerializeField] private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, enemiesSpawnPoint.Count);
        for (int i = 0; i <= enemyNum; i++)
        {
            int randEnemy = Random.Range(0, EnemyPool.instance.enemyType.Count);
            GameObject enemy = Instantiate(EnemyPool.instance.enemyType[randEnemy], enemiesSpawnPoint[rand].transform.position,Quaternion.identity,enemyParent.transform);
            enemy.GetComponent<Enemy>().MyRoom = this;
            enemiesSpawnPoint[rand].isOccupied = true;
            occupiedSpawnPoint.Add(enemiesSpawnPoint[rand]);
            enemiesSpawnPoint.Remove(enemiesSpawnPoint[rand]);
            rand = Random.Range(0, enemiesSpawnPoint.Count);
        }
        
        foreach (var door in _doors)
        {
            door.enemySpawned = true;
        }

        Invoke(nameof(CountEnemy), 2f);
    }

    public void OpenDoors()
    {
        foreach (var door in _doors)
        {
            door.enemySpawned = false;
        }   
    }
    
    public void EnemyDeath()
    {
        if (enemys > 0)
        {
            enemys--;
        }
        
        if (enemys <= 0)
        {
            OpenDoors();
        }
    }

    private void CountEnemy()
    {
        enemys = this.gameObject.GetComponentsInChildren<Entity>().Length;
    }
}
