using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerDoor : MonoBehaviour
{
    private List<Collider> _colliders = new List<Collider>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        foreach (var item in GetComponentsInChildren<Collider>())
        {
            if (item.transform != transform)
            {
                _colliders.Add(item);
            }
        }

        if (_colliders.Count == 0)
        {
            Debug.LogError("No collider found");
        }
        
    }

    internal void CloseTheDoors()
    {
        foreach (var item in _colliders)
        {
            item.enabled = false;
        }
    }
}
