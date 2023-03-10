using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public GameObject roomNord;
    public GameObject roomSouth;
    public GameObject roomEst;
    public GameObject roomWest;

    [SerializeField]private List<RoomDetection> roomDetection;


    public void InitRoomDetector()
    {
        foreach (var room in roomDetection)
        {
            room.InitDoor();
        }
    }
}
