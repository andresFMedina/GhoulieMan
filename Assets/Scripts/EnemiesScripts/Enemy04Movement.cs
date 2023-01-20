using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Movement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        vel.x = -moveSpeed;
        rb.velocity = vel;
    }
}
