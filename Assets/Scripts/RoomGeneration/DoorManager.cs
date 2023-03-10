using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]private int direction;
    private RoomData roomData;
    private GameObject currentRoom;
    private GameObject player;
    private Vector3 playerPos;

    private void Start()
    {
        currentRoom = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        roomData = currentRoom.GetComponent<RoomData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("PORTA");
            switch (direction)
            {
                case 1:
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 5f);
                    roomData.roomNord.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 2:
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 5f);
                    roomData.roomSouth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 3:
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x + 5f,playerPos.y,playerPos.z);
                    roomData.roomEst.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                case 4:
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x - 5f,playerPos.y,playerPos.z);
                    roomData.roomWest.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
            }
        }
    }
}
