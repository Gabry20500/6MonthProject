using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
