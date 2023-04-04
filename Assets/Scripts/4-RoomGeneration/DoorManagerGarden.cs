using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManagerGarden : MonoBehaviour
{
    [SerializeField]private int direction;
    private GardenData gardenData;
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
        gardenData = currentRoom.GetComponent<GardenData>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemySpawned == false)
        {
            var cameraPosition = mainCamera.transform.position;
            switch (direction) {
                case 1:
                    
                    cameraPosition = gardenData.roomNorth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 15f);
                    
                    gardenData.roomNorth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 2:
                    
                    cameraPosition = gardenData.roomSouth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 15f);
                    
                    gardenData.roomSouth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 3:
                    
                    cameraPosition = gardenData.roomEst.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x + 15f,playerPos.y,playerPos.z);
                    
                    gardenData.roomEst.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 4:
                    
                    cameraPosition = gardenData.roomWest.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x - 15f,playerPos.y,playerPos.z);
                    
                    gardenData.roomWest.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
            }
        }
    }
}
