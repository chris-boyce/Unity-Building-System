using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlacement : MonoBehaviour
{
    
    void Start()
    {
        Debug.Log("Has Spawned");
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (Physics.ComputePenetration(GetComponent<Collider>(), transform.position, transform.rotation, collision.collider, collision.transform.position, collision.transform.rotation, out var direction, out var distance))
        {
            gameObject.transform.position += direction * distance;
            Debug.Log("Collision Detected");
        }
    }

    void OnTriggerEnter()
    {
        Debug.Log("Trigger Called");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
