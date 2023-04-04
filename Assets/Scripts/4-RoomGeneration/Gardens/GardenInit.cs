using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenInit : MonoBehaviour
{
    [SerializeField] private int direction;
    private GardenData gardenData;
    private Collider[] hitCollider;
    private void Start()
    {
        gardenData = GetComponentInParent<GardenData>();
    }

    public void InitGardenDoor()
    {
        hitCollider = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        foreach (var collider in hitCollider)
        {
            if (collider.CompareTag("Door"))
            {
                GameObject currentRoom = collider.gameObject;
                switch (direction)
                {
                    case 1:
                        gardenData.roomNorth = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        //Destroy(this.gameObject);
                        break;
                    case 2:
                        gardenData.roomSouth = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        //Destroy(this.gameObject);
                        break;
                    case 3:
                        gardenData.roomEst = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        //Destroy(this.gameObject);
                        break;
                    case 4:
                        gardenData.roomWest = currentRoom.transform.parent.gameObject.transform.parent.gameObject;
                        //Destroy(this.gameObject);
                        break;
                }
            }
        }
    }
}
