using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private int direction;
    private RoomData roomData;
    private Collider[] hitCollider;
    private void Start()
    {
        roomData = GetComponentInParent<RoomData>();
    }

    public void InitDoor()
    {
        hitCollider = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        foreach (var collider in hitCollider)
        {
            if (collider.CompareTag("Ground") || collider.CompareTag("Garden"))
            {
                GameObject currentRoom = collider.gameObject;
                switch (direction)
                {
                    case 1:
                        roomData.roomNord = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        roomData.roomSouth = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 3:
                        roomData.roomEst = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 4:
                        roomData.roomWest = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                }
            }
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Ground"))
    //     {
    //         switch (direction)
    //             {
    //                 case 1:
    //                     roomData.roomNord = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
    //                     Destroy(this.gameObject);
    //                     break;
    //                 case 2:
    //                     roomData.roomSouth = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
    //                     Destroy(this.gameObject);
    //                     break;
    //                 case 3:
    //                     roomData.roomEst = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
    //                     Destroy(this.gameObject);
    //                     break;
    //                 case 4:
    //                     roomData.roomWest = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
    //                     Destroy(this.gameObject);
    //                     break;
    //             
    //             }
    //     }
    // }
    
}
