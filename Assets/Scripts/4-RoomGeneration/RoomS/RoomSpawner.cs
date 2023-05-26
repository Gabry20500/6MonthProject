using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    /*
     * 1 --> need down door
     * 2 --> need up door
     * 3 --> need left door
     * 4 --> need right door
     */
    
    private int rand;
    private bool spawned = false;
    
    
    private float waitTime = 0.1f;
    [SerializeField] bool spawnedGarden = false;
    
    private void Start()
    {
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        
        if (spawned == false)
        {
            GameObject newRoom;
            if (RoomTemplates.instance.currentRooms < RoomTemplates.instance.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, RoomTemplates.instance.downRooms.Length);
                    newRoom = Instantiate(RoomTemplates.instance.downRooms[rand], transform.position, RoomTemplates.instance.downRooms[rand].transform.rotation, RoomTemplates.instance.transform);
                    
                }else if (openingDirection == 2)
                {
                    rand = Random.Range(0, RoomTemplates.instance.topRooms.Length);
                    newRoom = Instantiate(RoomTemplates.instance.topRooms[rand], transform.position, RoomTemplates.instance.topRooms[rand].transform.rotation, RoomTemplates.instance.transform);
                }else if (openingDirection == 3)
                {
                    rand = Random.Range(0, RoomTemplates.instance.leftRooms.Length);
                    newRoom = Instantiate(RoomTemplates.instance.leftRooms[rand], transform.position, RoomTemplates.instance.leftRooms[rand].transform.rotation, RoomTemplates.instance.transform);
                }else if (openingDirection == 4)
                {
                    rand = Random.Range(0, RoomTemplates.instance.rightRooms.Length);
                    newRoom = Instantiate(RoomTemplates.instance.rightRooms[rand], transform.position, RoomTemplates.instance.rightRooms[rand].transform.rotation, RoomTemplates.instance.transform);
                }
                spawned = true;
                RoomTemplates.instance.currentRooms++;
            }
            else if (RoomTemplates.instance.currentRooms == RoomTemplates.instance.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    newRoom = Instantiate(RoomTemplates.instance.bossRooms[1], transform.position, RoomTemplates.instance.downRooms[0].transform.rotation, RoomTemplates.instance.transform);
                    StartCoroutine(RoomTemplates.instance.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                    
                }else if (openingDirection == 2)
                {
                    newRoom = Instantiate(RoomTemplates.instance.bossRooms[0], transform.position, RoomTemplates.instance.topRooms[0].transform.rotation, RoomTemplates.instance.transform);
                    StartCoroutine(RoomTemplates.instance.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                    
                }else if (openingDirection == 3)
                {
                    newRoom = Instantiate(RoomTemplates.instance.bossRooms[3], transform.position, RoomTemplates.instance.leftRooms[0].transform.rotation, RoomTemplates.instance.transform);
                    StartCoroutine(RoomTemplates.instance.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;

                }else if (openingDirection == 4)
                {
                    newRoom = Instantiate(RoomTemplates.instance.bossRooms[2], transform.position, RoomTemplates.instance.rightRooms[0].transform.rotation, RoomTemplates.instance.transform);
                    StartCoroutine(RoomTemplates.instance.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                }
                spawned = true;
                RoomTemplates.instance.currentRooms++;
            }
            else
            {
                Instantiate(RoomTemplates.instance.closedRoom, transform.position, Quaternion.identity, RoomTemplates.instance.transform);
            }
            
        }
    }
}
