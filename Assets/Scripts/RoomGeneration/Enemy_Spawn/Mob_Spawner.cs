using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob_Spawner : MonoBehaviour
{
    public int enemyNum;
    [SerializeField] private List<SpawnPointEnemy> enemiesSpawnPoint;
    [SerializeField] private List<SpawnPointEnemy> occupiedSpawnPoint;
    [SerializeField] private List<GameObject> _doors;
    [SerializeField] private GameObject enemyParent;
    private EnemyPool enemyPool;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = FindObjectOfType<EnemyPool>();
        int rand = Random.Range(0, enemiesSpawnPoint.Count);
        for (int i = 0; i < enemyNum; i++)
        {
            Instantiate(enemyPool.baseEnemy, enemiesSpawnPoint[rand].GameObject().transform.position,Quaternion.identity,enemyParent.transform);
            enemiesSpawnPoint[rand].isOccupied = true;
            occupiedSpawnPoint.Add(enemiesSpawnPoint[rand]);
            enemiesSpawnPoint.Remove(occupiedSpawnPoint[rand]);
            rand = Random.Range(0, enemiesSpawnPoint.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
