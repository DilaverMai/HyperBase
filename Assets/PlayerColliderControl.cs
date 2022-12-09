using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderControl : MonoBehaviour
{
    private TestStackTrail testStackTrail;

    private void Awake()
    {
        testStackTrail = GetComponent<TestStackTrail>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            Debug.Log("Player collided with cube");
            testStackTrail.AddItem(cube);
            GetComponent<Collider>().enabled = false;
        }
    }
}
