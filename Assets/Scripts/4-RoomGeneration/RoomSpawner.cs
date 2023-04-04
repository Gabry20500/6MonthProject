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

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    
    
    private float waitTime = 0.1f;
    [SerializeField] bool spawnedGarden = false;
    
    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        
        if (spawned == false)
        {
            GameObject newRoom;
            if (templates.currentRooms < templates.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, templates.downRooms.Length);
                    newRoom = Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation, templates.transform);
                }else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    newRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation, templates.transform);
                }else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    newRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation, templates.transform);
                }else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    newRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, templates.transform);
                }
                spawned = true;
                templates.currentRooms++;
            }
            else if (templates.currentRooms == templates.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    newRoom = Instantiate(templates.downRooms[0], transform.position, templates.downRooms[0].transform.rotation, templates.transform);
                    StartCoroutine(templates.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                }else if (openingDirection == 2)
                {
                    newRoom = Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, templates.transform);
                    StartCoroutine(templates.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                }else if (openingDirection == 3)
                {
                    newRoom = Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, templates.transform);
                    StartCoroutine(templates.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                }else if (openingDirection == 4)
                {
                    newRoom = Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, templates.transform);
                    StartCoroutine(templates.DeactiveRoom());
                    newRoom.GetComponent<Mob_Spawner>().enabled = false;
                }
                spawned = true;
                templates.currentRooms++;
            }
            else
            {
                SpawnGarden();
            }
            
        }
    }

    void SpawnGarden()
    { 
        Instantiate(templates.closedRoom, transform.position, Quaternion.identity, templates.transform);
        spawnedGarden = true;
    }
}
