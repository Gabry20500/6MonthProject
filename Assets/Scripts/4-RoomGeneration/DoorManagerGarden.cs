using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    
    [Header("Slider")] 
    [SerializeField] private Slider doorSlider;
    private float startValue = 0f;
    private float targetValue = 1f;
    [SerializeField]private bool canGo = false;
    
    
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentRoom = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        gardenData = currentRoom.GetComponent<GardenData>();
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemySpawned == false)
        {
            doorSlider.gameObject.SetActive(true);
        
            doorSlider.maxValue = 1f;
            doorSlider.value = 0f;
            StartCoroutine(GoInOtherRoom());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (canGo)
        { 
            if (other.gameObject.CompareTag("Player") && enemySpawned == false)
            {
                var cameraPosition = mainCamera.transform.position;
                switch (direction) {
                    case 1:
                        
                        cameraPosition = gardenData.roomNorth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                        mainCamera.transform.position = cameraPosition;
                        
                        playerPos = player.transform.position;
                        player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z + 25f);
                        
                        gardenData.roomNorth.SetActive(true);
                        currentRoom.SetActive(false);
                        break;
                    
                    case 2:
                        
                        cameraPosition = gardenData.roomSouth.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                        mainCamera.transform.position = cameraPosition;
                        
                        playerPos = player.transform.position;
                        player.transform.position = new Vector3(playerPos.x,playerPos.y,playerPos.z - 25f);
                        
                        gardenData.roomSouth.SetActive(true);
                        currentRoom.SetActive(false);
                        break;
                    
                    case 3:
                        
                        cameraPosition = gardenData.roomEst.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                        mainCamera.transform.position = cameraPosition;
                        
                        playerPos = player.transform.position;
                        player.transform.position = new Vector3(playerPos.x + 25f,playerPos.y,playerPos.z);
                        
                        gardenData.roomEst.SetActive(true);
                        currentRoom.SetActive(false);
                        break;
                    
                    case 4:
                        
                        cameraPosition = gardenData.roomWest.GetComponentInChildren<SetCameraPos>().gameObject.transform.position;
                        mainCamera.transform.position = cameraPosition;
                        
                        playerPos = player.transform.position;
                        player.transform.position = new Vector3(playerPos.x - 25f,playerPos.y,playerPos.z);
                        
                        gardenData.roomWest.SetActive(true);
                        currentRoom.SetActive(false);
                        break;
                    
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemySpawned == false)
        {
            doorSlider.value = 0f;
            startValue = doorSlider.value;
            canGo = false;
            doorSlider.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        doorSlider.value = 0f;
        startValue = doorSlider.value;
        canGo = false;
        doorSlider.gameObject.SetActive(false);
    }

    private IEnumerator GoInOtherRoom()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            doorSlider.value = Mathf.Lerp(startValue, targetValue, t);


            yield return null;
        }
        
        canGo = true;
        doorSlider.value = targetValue;
    }
}
