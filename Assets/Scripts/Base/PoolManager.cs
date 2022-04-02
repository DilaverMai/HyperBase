using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<PoolObject<Enum_PoolObject, PoolItem>>
    PoolObjects = new List<PoolObject<Enum_PoolObject, PoolItem>>();
    public static PoolManager Instance;

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

    public void AddObject(PoolItem poolObject)
    {
        PoolObjects[poolObject._poolID].AddObject(poolObject.gameObject);
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
    }
#endif

}

public static class PoolEvents
{
    public static PoolItem GetObject(this Enum_PoolObject poolObject)
    {
        foreach (var item in PoolManager.Instance.PoolObjects)
        {
            var itemName = item.Prefab.name.Split(' ');
            if (itemName[0] == poolObject.ToString())
            {
                return item.GetObject();
            }
        }

        return null;
    }


}

[System.Serializable]
public class PoolObject<T, F> where F : Component
{
    public T Enum;
    public GameObject Prefab;
    public List<GameObject> pool = new List<GameObject>();
    public int SpawnCount = 25;
    public bool isActive = false;
    public PoolObject(GameObject gameObject)
    {
        this.Prefab = gameObject;
        isActive = true;
    }

    public void AddObject(GameObject obj)
    {
        pool.Add(obj);
        obj.SetActive(false);
    }
    public void Setup()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            var spawned = GameObject.Instantiate(Prefab);

            if (spawned.GetComponent<F>() == null)
            {
                spawned.AddComponent<F>();
            }

            pool.Add(spawned);
            spawned.SetActive(false);

        }
    }

    public F GetObject()
    {
        GameObject obj = null;

        if (pool.Count > 0)
        {
            obj = pool[0];
            pool.RemoveAt(0);
            obj.SetActive(true);
            return obj.GetComponent<F>();
        }
        else
        {
            obj = GameObject.Instantiate(Prefab);
            pool.Add(obj);
            obj.SetActive(true);
            return obj.GetComponent<F>();
        }

    }
}
