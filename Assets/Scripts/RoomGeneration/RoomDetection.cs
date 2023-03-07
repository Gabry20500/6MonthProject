using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{
    [SerializeField] private int direction;
    private RoomData roomData;

    private void Start()
    {
        roomData = GetComponentInParent<RoomData>();
        StartCoroutine(Detect());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            switch (direction)
                {
                    case 1:
                        roomData.roomNord = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        roomData.roomSouth = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 3:
                        roomData.roomEst = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                    case 4:
                        roomData.roomWest = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        Destroy(this.gameObject);
                        break;
                
                }
        }
    }

    private IEnumerator Detect()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(true);
    }
}
