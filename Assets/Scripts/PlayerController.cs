using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData => PlayerData.Current;

    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    private Vector2 ClampX;
    private Vector2 ClampY;
    public float offsetY = 0.5f;
    private void Awake()
    {
        ClampX = new Vector2(playerData.ClampX.x, playerData.ClampX.y);
        ClampY = new Vector2(playerData.ClampY.x, playerData.ClampY.y);
    }

    public void Move(Vector2 target)
    {
        if (!Base.IsPlaying()) return;

        var targetToVector3 = new Vector3(target.x, target.y + offsetY, 0);

        var check = targetPos + targetToVector3;

        if (playerData.YMoving) check.y = Mathf.Clamp(check.y, ClampY.x, ClampY.y);
        else check.y = offsetY;
        if (playerData.XMoving) check.x = Mathf.Clamp(check.x, ClampX.x, ClampX.y);

        targetPos = check;
        //if(!playerData.YMoving) targetPos.y = transform.position.y;
    }

    private void Update()
    {
        if (!Base.IsPlaying()) return;

        targetPos.z += Time.fixedDeltaTime * playerData.MoveSpeed;

        transform.position = Vector3.Lerp(transform.position, targetPos, playerData.MoveSpeed * Time.deltaTime);
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
