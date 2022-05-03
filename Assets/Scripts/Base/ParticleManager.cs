using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // public List<PoolParticle> PoolParticles = new List<PoolParticle>();
    // public static ParticleManager Instance;
    // [HideInInspector]
    // public ParticleCamera particleCamera;

    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }

    //     particleCamera = FindObjectOfType<ParticleCamera>();
    // }

    // private void Start()
    // {
    //     foreach (var item in PoolParticles)
    //     {
    //         item.Setup();
    //     }
    // }

    // // public void BackToList(ParticleItem particleItem)
    // // {
    // //     foreach (var item in PoolParticles)
    // //     {
    // //         if (item.Enum == particleItem._Enum)
    // //         {
    // //             item.AddObject(particleItem.gameObject);
    // //             break;
    // //         }
    // //     }

    // // }

    // private void WinEffect(GameStat gameStat)
    // {

    //     if (gameStat == GameStat.Win)
    //     {
    //         CameraParticle.Confetti.Play();
    //     }

    // }


    // void OnEnable()
    // {
    //     EventManager.BeforeFinishGame += WinEffect;
    // }

    // void OnDisable()
    // {
    //     EventManager.BeforeFinishGame -= WinEffect;
    // }

}

public static class ParticleManagerExtension
{
    // public static ParticleItem GetParticle(this Enum_PoolParticle enum_PoolParticle,Vector3 pos = default(Vector3))
    // {
    //     var particleManager = ParticleManager.Instance;

    //     foreach (var item in particleManager.PoolParticles)
    //     {
    //         if (item.Enum == enum_PoolParticle)
    //         {
    //             return item.GetObject();
    //         }
    //     }

    //     Debug.LogError("Not found particle");
    //     return null;
    // }

    // public static void SetPosition(this ParticleItem particleItem,Vector3 pos)
    // {
    //     particleItem.transform.position = pos;
    // }

    // public static void DelayPlay(this ParticleItem particleItem,float delay)
    // {
    //     particleItem.DelayPlay(delay);
    // }

    // public static void SetRotation(this ParticleItem particle,Vector3 eulerAngles)
    // {
    //     ParticleManager.Instance.particleCamera.transform.eulerAngles = eulerAngles;
    // }

    // public static void SetParent(this ParticleItem particle,Transform trans)
    // {
    //     ParticleManager.Instance.particleCamera.transform.parent = trans;
    // }

}

// public class PoolParticle : AbstractPoolObject<ParticleItem>
// {
//     [HideInInspector]
//     public Enum_PoolParticle Enum;
// }
