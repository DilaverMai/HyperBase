using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJoyStick : MonoBehaviour
{
    [SerializeField]
    private float _rotSpeed = 1f;
    public float RotSpeed => _rotSpeed;
    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;
    [SerializeField]
    private float maxDistance;
    private Vector3 targetPosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        JoyStickMove();
    }

    private void JoyStickMove()
    {
        var posPower = Vector3.zero;

        posPower += JoyStick.Instance.GetVector();


        if (Vector3.Distance(transform.position, targetPosition) < maxDistance)
        {
            targetPosition += (posPower * _rotSpeed) * Time.deltaTime;
        }

        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position,
        targetPosition, Time.deltaTime * MoveSpeed);

    }

    //gizmos for targetposition
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPosition, 0.5f);
        Gizmos.DrawLine(transform.position, targetPosition);
    }

}
