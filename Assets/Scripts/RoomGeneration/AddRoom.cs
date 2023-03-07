using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomTemplates>();
        if (gameObject.CompareTag("Rooms"))
        {
            templates.rooms.Add(this.gameObject);
        }else if (gameObject.CompareTag("Garden"))
        {
            templates.gardens.Add(this.gameObject);
        }
        
    }
}
