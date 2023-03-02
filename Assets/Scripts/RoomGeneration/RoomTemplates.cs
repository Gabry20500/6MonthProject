using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoomTemplates : MonoBehaviour
{
    [Header("Rooms prefab")]
    public GameObject[] downRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    [Header("RoomSpawned")]
    public List<GameObject> rooms;

    [Header("Max rooms number")] 
    public int maxRoom;
    [NonSerialized]public int currentRooms = 0;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boos;

    private void Update()
    {
        if (waitTime <= 0 && !spawnedBoss)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count -1)
                {
                    Instantiate(boos, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else if (!spawnedBoss)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
