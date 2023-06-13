using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Stone_Object : MonoBehaviour
{
    [SerializeField] Stone stone;
    
    [Header("Levitation")]
    [SerializeField] private float levitationDistance = 1f;
    [SerializeField] private float levitationSpeed = 1f;
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position; 
    }

    private void Update()
    {
        Vector3 targetPosition =
            _originalPosition + Vector3.up * levitationDistance * Mathf.Sin(Time.time * levitationSpeed);

        transform.position = targetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Sword sw = collision.gameObject.GetComponent<EMovement>().Sword;
            bool flag = sw.PickUp_Stone(stone);
            if(flag == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
