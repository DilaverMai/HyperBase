using UnityEngine;

public class Player : Character
{
    private PlayerData playerData;
    protected override void Awake()
    {
        base.Awake();
        playerData = PlayerData.Current;
    }
    
    protected override void Start()
    {
        base.Start();
        Cameras.cam1.CameraSetFull(transform);
        Cameras.cam1.SetOffset(new Vector3(0, 10, -20));
    }
}