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
                Instantiate(boss, rooms[i].transform.position + (Vector3.up *4), Quaternion.identity, rooms[i].transform);
                spawnedBoss = true;

                var maxHp = boss.GetComponent<Boss>().MaxHealth;
                maxHp = LevelManager.instance.IncrementFloatStats(maxHp);
                boss.GetComponent<Boss>().MaxHealth = maxHp;
                boss.GetComponent<Boss>().Healt = maxHp;
                boss.GetComponent<Boss>().initBar();

                var bossRoom = rooms[i];
                bossDoor = bossRoom.GetComponentInChildren<DoorManager>();
                bossDoor.enemySpawned = true;


                foreach (var room in rooms)
                {
                    room.GetComponent<RoomData>().InitRoomDetector();
                }

                if (gardens.Count > 0)
                {
                    for(var j = gardens.Count - 1; j > -1; j--)
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

                foreach (var t in gardens)
                {
                    t.SetActive(false);
                }
            }
        }
    }

    public IEnumerator DeactiveRoom()
    {
        yield return new WaitForSeconds(3f);
        
            

    }
}
