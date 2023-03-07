using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
            Destroy(other.gameObject);

        if (other.CompareTag("Garden"))
            Destroy(other.gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
