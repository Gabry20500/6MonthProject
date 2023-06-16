using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoomTemplates : Singleton<RoomTemplates>
{
    [Header("Rooms prefab")]
    public GameObject[] downRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    [Header("RoomSpawned")]
    public List<GameObject> rooms;
    public List<GameObject> gardens;

    [Header("Max rooms number")] 
    public int maxRoom;
    [NonSerialized]public int currentRooms = 0;
    private bool allFinish = false;
    
    bool spawnedBoss;
    
    [Header("Boss room")] 
    public GameObject boss;
    public GameObject ladder;
    public GameObject[] bossRooms;
    private DoorManager bossDoor;
    

    private void Update()
    {
        if (rooms.Count >= maxRoom && !allFinish)
        {
            Invoke(nameof(FinalLevelSetup), .5f);
            allFinish = true;
        }
    }

    public void FinalLevelSetup()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == rooms.Count -1)
            {
                GameObject currentBoss = Instantiate(boss, rooms[i].transform.position + (Vector3.up *4), Quaternion.identity, rooms[i].transform);
                spawnedBoss = true;

                //Variable used for set Boos hp
                EnemyAI bossAi = currentBoss.GetComponent<EnemyAI>();
                float maxHp = bossAi.Enemy_Data.max_HP;
                
                maxHp = LevelManager.instance.IncrementFloatStats(maxHp);
                bossAi.Enemy_Data.max_HP = maxHp;
                bossAi.Enemy_Data.HP = maxHp;
                currentBoss.GetComponent<Enemy>().InitParameters(bossAi.Enemy_Data);


                GameObject bossRoom = rooms[i];
                bossDoor = bossRoom.GetComponentInChildren<DoorManager>();
                bossDoor.enemySpawned = true;


                foreach (GameObject room in rooms)
                {
                    room.GetComponent<RoomData>().InitRoomDetector();
                }

                if (gardens.Count > 0)
                {
                    for(int j = gardens.Count - 1; j > -1; j--)
                    {
                        if (gardens[j] == null)
                        {
                            gardens.RemoveAt(j);
                        }
                        else
                        {
                            gardens[j].GetComponent<GardenData>().InitGarden();
                        }
                            
                    }
                }

                for (int j = 1; j < rooms.Count; j++)
                {
                    rooms[j].SetActive(false);
                }

                foreach (GameObject t in gardens)
                {
                    t.SetActive(false);
                }
            }
        }
    }

    
}
