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

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentRoom = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        roomData = currentRoom.GetComponent<RoomData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            var cameraPosition = mainCamera.transform.position;
            switch (direction)
            {
                case 1:
                    
                    cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, CalcCameraPositionUp(roomData.roomNord.transform.position, cameraPosition).z);
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 15f);
                    
                    roomData.roomNord.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 2:
                    
                    cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, CalcCameraPositionDown(roomData.roomSouth.transform.position, cameraPosition).z);
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 15f);
                    
                    roomData.roomSouth.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 3:
                    
                    cameraPosition = new Vector3( roomData.roomEst.transform.position.x , cameraPosition.y, cameraPosition.z);
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x + 15f,playerPos.y,playerPos.z);
                    
                    roomData.roomEst.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
                case 4:
                    
                    cameraPosition = new Vector3( roomData.roomWest.transform.position.x, cameraPosition.y, cameraPosition.z);
                    mainCamera.transform.position = cameraPosition;
                    
                    playerPos = player.transform.position;
                    player.transform.position = new Vector3(playerPos.x - 15f,playerPos.y,playerPos.z);
                    
                    roomData.roomWest.SetActive(true);
                    currentRoom.SetActive(false);
                    break;
                
            }
        }
    }

    private Vector3 CalcCameraPositionUp(Vector3 room, Vector3 cam)
    {
        if (cam.z < 0)
        {
            return room + cam;    
        }
        
        return room - cam;
    }
    
    private Vector3 CalcCameraPositionDown(Vector3 room, Vector3 cam)
    {
        if (room.z < 0)
        {
            return cam + room;    
        }
        
        return room - cam;
    }
}
