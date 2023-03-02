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
    
    
    public float waitTime = 0.1f;
    [SerializeField] bool spawnedWall = false;
    
    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    private void Update()
    {
        if (waitTime <= 0 && !spawnedWall)
        {
            SpawnWall();
            spawnedWall = true;
        }
        else if (!spawnedWall)
        {
            waitTime -= Time.deltaTime;
        }
    }
    
    void Spawn()
    {
        if (spawned == false)
        {
            if (templates.currentRooms < templates.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, templates.downRooms.Length);
                    Instantiate(templates.downRooms[rand], transform.position, templates.downRooms[rand].transform.rotation);
                }else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                }else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                }else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation); 
                }
                spawned = true;
                templates.currentRooms++;
            }
            else if (templates.currentRooms == templates.maxRoom - 1)
            {
                if (openingDirection == 1)
                {
                    Instantiate(templates.downRooms[0], transform.position, templates.downRooms[0].transform.rotation);
                }else if (openingDirection == 2)
                {
                    Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation);
                }else if (openingDirection == 3)
                {
                    Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation);
                }else if (openingDirection == 4)
                {
                    Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation); 
                }
                spawned = true;
                templates.currentRooms++;
            }
            
        }
    }

    void SpawnWall()
    {
        Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned && !spawned)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
