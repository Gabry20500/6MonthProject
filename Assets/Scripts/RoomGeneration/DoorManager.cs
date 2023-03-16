using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]private int direction;
    private RoomData roomData;
    private GameObject currentRoom;
    private GameObject player;
    private Vector3 playerPos;
    
    private GameObject mainCamera;
    private Transform CameraPos;
    public bool enemySpawned;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentRoom = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        roomData = currentRoom.GetComponent<RoomData>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         var cameraPosition = mainCamera.transform.position;
    //         switch (direction)
    //         {
    //             case 1:
    //                 
    //                 cameraPosition = roomData.roomNord.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
    //                 mainCamera.transform.position = cameraPosition;
    //                 
    //                 playerPos = player.transform.position;
    //                 player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 15f);
    //                 
    //                 roomData.roomNord.SetActive(true);
    //                 currentRoom.SetActive(false);
    //                 break;
    //             
    //             case 2:
    //                 
    //                 cameraPosition = roomData.roomSouth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
    //                 mainCamera.transform.position = cameraPosition;
    //                 
    //                 playerPos = player.transform.position;
    //                 player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 15f);
    //                 
    //                 roomData.roomSouth.SetActive(true);
    //                 currentRoom.SetActive(false);
    //                 break;
    //             
    //             case 3:
    //                 
    //                 cameraPosition = roomData.roomEst.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
    //                 mainCamera.transform.position = cameraPosition;
    //                 
    //                 playerPos = player.transform.position;
    //                 player.transform.position = new Vector3(playerPos.x + 15f,playerPos.y,playerPos.z);
    //                 
    //                 roomData.roomEst.SetActive(true);
    //                 currentRoom.SetActive(false);
    //                 break;
    //             
    //             case 4:
    //                 
    //                 cameraPosition = roomData.roomWest.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
    //                 mainCamera.transform.position = cameraPosition;
    //                 
    //                 playerPos = player.transform.position;
    //                 player.transform.position = new Vector3(playerPos.x - 15f,playerPos.y,playerPos.z);
    //                 
    //                 roomData.roomWest.SetActive(true);
    //                 currentRoom.SetActive(false);
    //                 break;
    //             
    //         }
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemySpawned == false)
        {
            var cameraPosition = mainCamera.transform.position;
            switch (direction) {
                case 1:
                    
                    cameraPosition = roomData.roomNord.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 15f);
                    
                    roomData.roomNord.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 2:
                    
                    cameraPosition = roomData.roomSouth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 15f);
                    
                    roomData.roomSouth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 3:
                    
                    cameraPosition = roomData.roomEst.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x + 15f,playerPos.y,playerPos.z);
                    
                    roomData.roomEst.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 4:
                    
                    cameraPosition = roomData.roomWest.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x - 15f,playerPos.y,playerPos.z);
                    
                    roomData.roomWest.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
            }
        }
    }
}
