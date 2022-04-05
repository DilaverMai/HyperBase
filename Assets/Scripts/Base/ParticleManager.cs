using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<PoolParticle> PoolParticles = new List<PoolParticle>();
    public static ParticleManager Instance;

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
}

public class PoolParticle: AbstractPoolObject<ParticleItem>
{
    public Enum_PoolParticle Enum;
}
