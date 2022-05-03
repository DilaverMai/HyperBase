using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;
using System;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera[] cinemachineVirtualCameras;
    [SerializeField]
    private List<VirtualCamera> virtualCameras = new List<VirtualCamera>();
    public static CameraManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cinemachineVirtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        Array.Reverse(cinemachineVirtualCameras);

        for (int i = 0; i < cinemachineVirtualCameras.Length; i++)
        {
            var vCamera = new VirtualCamera(cinemachineVirtualCameras[i], (Cameras)i);
            virtualCameras.Add(vCamera);
        }

    }

    private void HardResetPosition()
    {
        foreach (var item in virtualCameras)
        {
            item.HardReset();
        }
    }

    public void SetFocusCam(Cameras camera)
    {
        foreach (var item in virtualCameras)
        {
            if (item.camera == camera)
            {
                item.virtualCamera.Priority = 1;
            }
            else
            {
                item.virtualCamera.Priority = 0;
            }
        }
    }

    private void OnEnable()
    {
        EventManager.OnBeforeLoadedLevel += HardResetPosition;
    }

    private void OnDisable()
    {
        EventManager.OnBeforeLoadedLevel -= HardResetPosition;
    }

    public CinemachineVirtualCamera GetCinemachineVirtualCamera(Cameras camera)
    {
        return virtualCameras[(int)camera].virtualCamera;
    }

    public VirtualCamera GetVirtualCamera(Cameras camera)
    {
        return virtualCameras[(int)camera];
    }

    public Transform GetCameraTransform(Cameras camera)
    {
        return virtualCameras[(int)camera].virtualCamera.transform;
    }
}

public static class CameraExtension
{
    public static VirtualCamera GetVirtualCamera(this Cameras camera)
    {
        return CameraManager.Instance.GetVirtualCamera(camera);
    }
    public static void SetFocusCam(this Cameras camera)
    {
        CameraManager.Instance.SetFocusCam(camera);
    }

    public static void SetFollow(this Cameras camera, Transform target)
    {
        CameraManager.Instance.GetVirtualCamera(camera).SetFollow(target);
    }

    public static Transform GetCameraTransform(this Cameras camera)
    {
        return CameraManager.Instance.GetCameraTransform(camera);
    }

    public static void SetAim(this Cameras camera, Transform target)
    {
        CameraManager.Instance.GetVirtualCamera(camera).SetAim(target);
    }

    public static void SetFull(this Cameras camera, Transform target)
    {
        CameraManager.Instance.GetVirtualCamera(camera).SetCamera(target);
    }

    public static void SetOffset(this Cameras camera, Vector3 offset)
    {
        CameraManager.Instance.GetVirtualCamera(camera).SetOffset(offset);
    }


}

[System.Serializable]
public class VirtualCamera
{
    public CinemachineVirtualCamera virtualCamera;
    public Cameras camera;

    public VirtualCamera(CinemachineVirtualCamera virtualCamera, Cameras camera)
    {
        this.virtualCamera = virtualCamera;
        this.camera = camera;
    }

    public void SetCamera(Transform target)
    {
        virtualCamera.Follow = target;
        virtualCamera.LookAt = target;
    }

    public void SetAim(Transform target)
    {
        virtualCamera.LookAt = target;
    }

    public void SetFollow(Transform target)
    {
        virtualCamera.Follow = target;
    }

    public void SetOffset(Vector3 offset)
    {
        var body = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        body.m_FollowOffset = offset;
    }

    public async void Shake(float intensity, float duration, float time)
    {
        var body = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        body.m_AmplitudeGain = intensity;
        body.m_FrequencyGain = duration;

        await Task.Delay((int)time * 1000);

        body.m_AmplitudeGain = 0;
        body.m_FrequencyGain = 0;
    }

    public void HardReset()
    {
        var target = virtualCamera.Follow;
        virtualCamera.Follow = null;
        virtualCamera.LookAt = null;
        virtualCamera.transform.position = Vector3.zero;
        if (target != null) SetCamera(target);
        Cameras.cam1.SetFocusCam();
    }
}
