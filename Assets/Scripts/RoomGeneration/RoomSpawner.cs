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
    
    
    public float waitTime = 4f;
    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn()
    {
        if (spawned == false && templates.currentRooms < templates.maxRoom)
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
