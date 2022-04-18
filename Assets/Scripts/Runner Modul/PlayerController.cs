using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Touch;
public class PlayerController : MonoBehaviour
{
    private PlayerData playerData => PlayerData.Current;

    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    [SerializeField]
    private Vector3 targetRot = Vector3.zero;
    private Vector2 ClampX;
    private Vector2 ClampY;
    public float offsetY = 0.5f;
    private void Awake()
    {
        ClampX = new Vector2(playerData.ClampX.x, playerData.ClampX.y);
        ClampY = new Vector2(playerData.ClampY.x, playerData.ClampY.y);

        firstPosition = transform.position;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        LeanTouch.OnFingerUp -= OnFingerUp;
    }

    public void Move(Vector2 target)
    {
        if (!Base.IsPlaying() | !playerData.Move) return;
        RotationMove(target);

        var targetToVector3 = Vector3.zero;
        //new Vector3(target.x, target.y + offsetY, 0);
        if (playerData.XMoving) targetToVector3.x = target.x;
        if (playerData.YMoving) targetToVector3.y = target.y + offsetY;
        targetToVector3.z = 0;

        var check = targetPos + targetToVector3;

        if (playerData.YMoving) check.y = Mathf.Clamp(check.y, ClampY.x, ClampY.y);
        else check.y = offsetY;
        if (playerData.XMoving) check.x = Mathf.Clamp(check.x, ClampX.x, ClampX.y);

        targetPos = check;
        //if(!playerData.YMoving) targetPos.y = transform.position.y;
    }

    private void OnFingerUp(LeanFinger finger)
    {
        if (!Base.IsPlaying() | !playerData.MoveRotation) return;
        targetRot = Vector3.zero;
    }
    public void RotationMove(Vector2 rot)
    {
        if (playerData.MoveRotation)
        {
            targetRot = new Vector3(0, playerData.RotationSpeed * rot.x, 0);

            if (Mathf.Abs(targetRot.y) > playerData.MaxYangle)
            {
                if (targetRot.y > 0) targetRot.y = playerData.MaxYangle;
                else targetRot.y = -playerData.MaxYangle;
            }
        }
    }

    private void Update()
    {
        if (!Base.IsPlaying()) return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, 0.5f, targetPos.z), playerData.MoveSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 
        playerData.RotationTurnSpeed * Time.deltaTime);
        //Vector3.Lerp(transform.eulerAngles, targetRot, Time.deltaTime * playerData.MoveSpeed);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (!Base.IsPlaying()) return;
        if (playerData.ZMoving) targetPos.z += Time.fixedDeltaTime * playerData.MoveSpeed;
    }

    private Vector3 firstPosition;
    public void ResetPos()
    {
        targetPos = firstPosition;
        transform.position = firstPosition;
    }

    //Gizmos for targetPos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.5f);

        //gizmos for distance between player and targetPos
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }

}
