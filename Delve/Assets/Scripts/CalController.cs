using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalController : MonoBehaviour
{
    private float originalY;
    
    [SerializeField] private float speed;

    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 temp = new Vector3(target.position.x - 1, target.position.y, target.position.z);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x-1, target.position.y), speed * Time.deltaTime);
        if (transform.position == temp)
        {
            this.originalY = this.transform.position.y;
            
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && transform.position == temp)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, originalY + ((float)Mathf.Sin(Time.time*4) * 0.1f), pos.z);
        }
    }

}
