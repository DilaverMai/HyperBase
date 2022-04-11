using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<PoolObject> PoolObjects = new List<PoolObject>();
    public List<ParticleItem> PoolParticles = new List<ParticleItem>();
    public static PoolManager Instance;
    [HideInInspector]
    public Transform holdPool;
    private void Awake()
    {
        holdPool = new GameObject("Pool").transform;
        holdPool.SetParent(transform);


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartPool()
    {
        foreach (var item in PoolObjects)
        {
            item.Setup(holdPool,item.Enum);
        }
    }


    public void BackToList(PoolItem poolItem)
    {
        foreach (var item in PoolObjects)
        {
            if (item.Enum == poolItem._PoolEnum)
            {
                item.AddObject(poolItem.gameObject);
                break;
            }
        }
    }

    public void ReLoad()
    {
        foreach (Transform item in holdPool)
        {
            item.gameObject.SetActive(false);
        }        
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventManager.OnBeforeLoadedLevel += ReLoad;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        EventManager.OnBeforeLoadedLevel -= ReLoad;
    }

#if UNITY_EDITOR
    [Button]
    private void CreateEnums()
    {
        EnumCreator.CreateEnum("PoolObject",
         PoolObjects.Select(x => x.Prefab.name).ToArray());

        for (int i = 0; i < PoolObjects.Count; i++)
        {
            PoolObjects[i].Enum = (Enum_PoolObject)i;
        }

        EnumCreator.CreateEnum("PoolParticle",
        PoolParticles.Select(x => x.name).ToArray());

        for (int i = 0; i < PoolParticles.Count; i++)
        {
            PoolParticles[i]._Enum = (Enum_PoolParticle)i;
        }
    }
#endif

}

public static class PoolEvents
{
    public static PoolItem GetObject(this Enum_PoolObject poolObject)
    {
        foreach (var item in PoolManager.Instance.PoolObjects)
        {

            if (item.Enum == poolObject)
            {
                //item.GetObject().gameObject.SetActive(true);
                var obj = item.GetObject();
                obj.SetEnum(poolObject);
                return obj;
            }
        }

        return null;
    }


}

[System.Serializable]
public class PoolObject : AbstractPoolObject<PoolItem>
{
    [HideInInspector]
    public Enum_PoolObject Enum;
}
