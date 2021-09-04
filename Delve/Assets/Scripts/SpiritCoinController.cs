using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritCoinController : MonoBehaviour
{
    private float originalY;

    [SerializeField] private float floatStrength = 1; // You can change this in the Unity Editor to 
    // change the range of y positions that are possible.

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, originalY + ((float)Mathf.Sin(Time.time) * floatStrength), pos.z);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        other.gameObject.GetComponent<PlayerController>().addCoin();
    }
}
