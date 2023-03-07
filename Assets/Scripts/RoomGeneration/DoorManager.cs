using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]private int direction;
    private RoomData roomData;
    private GameObject currentRoom;

    private void Start()
    {
        currentRoom = GameObject.FindGameObjectWithTag("Rooms");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomData = GetComponentInParent<RoomData>();
            switch (direction)
            {
                case 1:
                    roomData.roomNord.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 2:
                    roomData.roomSouth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 3:
                    roomData.roomEst.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 4:
                    roomData.roomWest.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
            }
        }
    }
}
