using System;
using DG.Tweening;
using UnityEngine;

public class PlayerControllerLerpLeftRight: MonoBehaviour,IPlayerController
{
    [Header("Move")]
    public Vector3 TargetPos;
    public float XClamp;
    [Header("Speeds")]
    public float TurnSpeed;
    public float MoveSpeed;

    private Character baseCharacter;

    private void Awake()
    {
        baseCharacter = GetComponent<Character>();
    }

    public void MoveLeftRight(Vector2 direction)
    {
        TargetPos.x += direction.x;
        TargetPos.x = Mathf.Clamp(TargetPos.x, -XClamp, XClamp);
    }

    public void Move(ref float extraSpeed)
    {
        TargetPos.z += (MoveSpeed + extraSpeed) * Time.fixedDeltaTime;
        
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.fixedDeltaTime * TurnSpeed);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,TargetPos);
        Gizmos.DrawSphere(TargetPos, 0.25f);
    }
}