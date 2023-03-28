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
    public List<GameObject> gardens;

    [Header("Max rooms number")] 
    public int maxRoom;
    [NonSerialized]public int currentRooms = 0;
    private bool allFinish = false;
    
    bool spawnedBoss;
    public GameObject boos;

    private void Update()
    {
        if (rooms.Count >= maxRoom && !allFinish)
        {
            Invoke(nameof(FinishLevel), .5f);
            allFinish = true;
        }
    }

    public void FinishLevel()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (i == rooms.Count -1)
            {
                Instantiate(boos, rooms[i].transform.position, Quaternion.identity, rooms[i].transform);
                spawnedBoss = true;


                foreach (var room in rooms)
                {
                    room.GetComponent<RoomData>().InitRoomDetector();
                }
                
                foreach (var garden in gardens)
                {
                    garden.GetComponent<GardenData>().InitGarden();
                }


                for (int j = 1; j < rooms.Count; j++)
                {
                    rooms[j].SetActive(false);
                }

                foreach (var t in gardens)
                {
                    t.SetActive(false);
                }
            }
        }
    }

    public IEnumerator DeactiveRoom()
    {
        yield return new WaitForSeconds(3f);
        
            

    }
}
