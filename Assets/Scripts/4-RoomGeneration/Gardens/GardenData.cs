using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenData : MonoBehaviour
{
    
    [Header("Connected rooms")]
    public GameObject roomNorth;
    public GameObject roomSouth;
    public GameObject roomEst;
    public GameObject roomWest;
    
    
    [Header("Door reference")]
    [SerializeField]private GameObject doorNorth;
    [SerializeField]private GameObject doorSouth;
    [SerializeField]private GameObject doorEst;
    [SerializeField]private GameObject doorWest;
    
    [Header("Door initializer")]
    [SerializeField]private List<GardenInit> gardenInits;


    public void InitGarden()
    {
        foreach (var garden in gardenInits)
        {
            garden.InitGardenDoor();
        }
        
        ActivateDoor(roomNorth, doorNorth);
        ActivateDoor(roomSouth, doorSouth);
        ActivateDoor(roomEst, doorEst);
        ActivateDoor(roomWest, doorWest);
    }

    private void ActivateDoor(GameObject room, GameObject door)
    {
        if (room != null)
        {
            door.SetActive(true);
        } 
    }
}
