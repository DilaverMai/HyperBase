using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraManager;
using static Enums;

public class Player : MonoBehaviour
{
    private PlayerData data => PlayerData.Current;

    private void Awake() {
        Cameras.cam1.GetVirtualCamera().SetCamera(transform);
        Cameras.cam1.GetVirtualCamera().SetOffset(new Vector3(0, 3.5f, -10));
    }
}
