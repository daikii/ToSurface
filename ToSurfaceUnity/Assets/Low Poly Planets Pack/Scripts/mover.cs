using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    private Rigidbody rb;

    public float Speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * Speed;
    }

}
