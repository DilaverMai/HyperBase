using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<PoolParticle> PoolParticles = new List<PoolParticle>();
    public static ParticleManager Instance;
    [HideInInspector]
    public ParticleCamera particleCamera;

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

        particleCamera = FindObjectOfType<ParticleCamera>();
    }

    private void Start()
    {
        foreach (var item in PoolParticles)
        {
            item.Setup();
        }
    }

    public void BackToList(ParticleItem particleItem)
    {
        foreach (var item in PoolParticles)
        {
            if (item.Enum == particleItem._Enum)
            {
                item.AddObject(particleItem.gameObject);
                break;
            }
        }

    }

    private void WinEffect(Enums.GameStat gameStat)
    {

        if (gameStat == Enums.GameStat.Win)
        {
            Enums.CameraParticle.Confetti.ConfettiPlay();
        }

    }


    void OnEnable()
    {
        EventManager.BeforeFinishGame += WinEffect;
    }

    void OnDisable()
    {
        EventManager.BeforeFinishGame -= WinEffect;
    }

}

public static class ParticleManagerExtension
{
    public static ParticleItem PlayParticle(this Enum_PoolParticle enum_PoolParticle)
    {
        var particleManager = ParticleManager.Instance;

        foreach (var item in particleManager.PoolParticles)
        {
            if (item.Enum == enum_PoolParticle)
            {
                return item.GetObject();
            }
        }

        Debug.LogError("Not found particle");
        return null;
    }

    public static void ConfettiPlay(this Enums.CameraParticle particleManager)
    {
        ParticleManager.Instance.particleCamera.PlayParticle(Enums.CameraParticle.Confetti);
    }
}

public class PoolParticle : AbstractPoolObject<ParticleItem>
{
    [HideInInspector]
    public Enum_PoolParticle Enum;
}
