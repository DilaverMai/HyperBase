using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = 9.8f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void GravityForce()
    {
        var pos = transform.position;
        pos.y = gravity;
        transform.position -= pos * Time.deltaTime;
    }
}
