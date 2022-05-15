using UnityEngine;
using Lean.Touch;

public abstract class PlayerController : MonoBehaviour
{
    protected PlayerData playerData => PlayerData.Current;
    protected Vector3 firstPosition;

    [SerializeField]
    protected Vector3 targetPos = Vector3.zero;
    [SerializeField]
    protected Vector3 targetRot = Vector3.zero;
    public float offsetY = 0.5f;

    protected virtual void Awake()
    {
        firstPosition = transform.position;
    }

    public void ResetPos()
    {
        targetPos = firstPosition;
        transform.position = firstPosition;
    }


    //Gizmos for targetPos
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.5f);

        //gizmos for distance between player and targetPos
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }

    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerDown += OnFingerDown;

    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerUp -= OnFingerUp;
        LeanTouch.OnFingerDown -= OnFingerDown;
    }

    protected virtual void OnFingerDown(LeanFinger obj)
    {
        if (!Base.IsPlaying()) return;
        Debug.Log("Touch Down");
    }

    protected virtual void OnFingerUp(LeanFinger obj)
    {
        if (!Base.IsPlaying()) return;
        Debug.Log("Touch Up");
    }

    protected virtual void Update()
    {
        if (!Base.IsPlaying()) return;
    }

    protected virtual void FixedUpdate()
    {
        if (!Base.IsPlaying()) return;
    }

    protected virtual void FinishLine()
    {

    }

}
