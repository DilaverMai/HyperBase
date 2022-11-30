using System;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static Action OnTouchDown;
    public static Action OnTouchUp;
    public static Action OnTouchUpdate;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
