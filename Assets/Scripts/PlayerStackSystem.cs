using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public abstract class PlayerStackSystem<T>: MonoBehaviour where T : MonoBehaviour
{
    [Header("Stack")]
    public Transform StackParent;
    public float StackSpeed;
    public Vector3 StackOffset;
    public Vector3 StackGap;
    [Space]
    [Tooltip("Baska bir script ile listenin icine obje atayin.")]
    public List<T> StackList = new List<T>();
    [Space]
    [Header("Limits")]
    public float XClamp;
    public bool AddLast;
    
    [Header("Delta")]
    public Vector2 TouchDelta;
    public float DeltaMultiplier = 1f;
    
    protected Coroutine zeroCoroutine;
    public virtual void AddItem(T item)
    {
        if(StackList.Contains(item))
            return;
        
        item.transform.SetParent(StackParent);

        if (AddLast)
            StackList.Insert(0,item);
        else StackList.Add(item);
    }
    
    public virtual void RemoveItem(T item)
    {
        item.transform.SetParent(null);
        StackList.Remove(item);
    }
    
    public virtual void ClearStack()
    {
        StackList.Clear();
    }
    
    public virtual void UpdateStack(Vector2 touchDelta)
    {
        if (touchDelta.x == 0)
        {
            TouchDelta.x = Mathf.Lerp(TouchDelta.x ,0,StackSpeed * Time.fixedDeltaTime);
            return;
        }
        
        TouchDelta += touchDelta * (Time.fixedDeltaTime * DeltaMultiplier);
        TouchDelta.x = Mathf.Clamp(TouchDelta.x, -XClamp, XClamp);
    }
    
    protected virtual void MoveItems()
    {
        for (var index = 0; index < StackList.Count; index++)
        {
            var stackItem = StackList[index];

            var localPosition = stackItem.transform.localPosition;
            var pos = localPosition;

            pos.x = index * TouchDelta.x + StackOffset.x;
            pos.z = index * StackGap.z + StackOffset.z;
            localPosition = Vector3.Lerp(localPosition, pos, StackSpeed * Time.fixedDeltaTime);

            stackItem.transform.localPosition = localPosition;
        }
    }
    
    protected virtual void OnFingerUp(LeanFinger obj)
    {
        zeroCoroutine = StartCoroutine(ZeroTouchDelta());
    }
    
    protected virtual void OnFingerDown(LeanFinger obj)
    {
        if (zeroCoroutine == null) return;
        
        StopCoroutine(zeroCoroutine);
        zeroCoroutine = null;
    }
    
    protected virtual IEnumerator ZeroTouchDelta()
    {
        while (TouchDelta.x > 0)
        {
            TouchDelta.x = Mathf.Lerp(TouchDelta.x ,0,StackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        TouchDelta.x = 0;
        zeroCoroutine = null;
    } 

    private void FixedUpdate()
    {
        MoveItems();
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerDown += OnFingerDown;
    }
    
    private void OnDisable()
    {
        LeanTouch.OnFingerUp -= OnFingerUp;
        LeanTouch.OnFingerDown -= OnFingerDown;
    }
}