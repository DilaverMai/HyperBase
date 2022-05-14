using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public UnityEvent ExtraFunc = new UnityEvent();
    public LayerMask DetectedMask;
    private Collider _collider;
    
    void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(Vector3 _vel)
    {
        _rigidbody.velocity = _vel;
        ExtraFunc.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((DetectedMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }
}
