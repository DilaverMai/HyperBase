using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, Pool> Pools = new Dictionary<string, Pool>();
    public Transform PoolParent;
    public GameObject[] ResourcePrefabs;
    
    public Task Setup()
    {
        PoolParent = new GameObject("PoolParent").transform;
        PoolParent.SetParent(transform);
        PoolParent.position = Vector3.zero;


        return Task.CompletedTask;
    }
    
    public string[] GetPoolNames()
    {
        return Pools.Keys.ToArray();
    }
    
    
    [Button]
    private void CreateEnumPools()
    {
        ResourcePrefabs = Resources.LoadAll<GameObject>("Prefabs");
        
        var poolNames = ResourcePrefabs.Select(x => x.name).ToArray();
        
        EnumCreator.CreateEnum("PoolObjects",poolNames);
    }

    public GameObject GetPoolItem(GameObject obj)
    {
        if (Pools.ContainsKey(obj.name))
        {
            return Pools[obj.name].GetPoolItem();
        }

        var pool = new Pool(obj);
        Pools.Add(obj.name, pool);
        Debug.Log("New pool Created " + obj.name);
        return pool.GetPoolItem();
    }

    public void ReturnPoolItem(GameObject obj)
    {
        if (Pools.ContainsKey(obj.name))
        {
            Pools[obj.name].ReturnPoolItem(obj);
            return;
        }

        Debug.LogWarning("No pools found for " + obj.name);
    }
    
    public GameObject GetResourcePrefab(PoolObjects name)
    {
        foreach (var resourcePrefab in ResourcePrefabs)
        {
            if (resourcePrefab.name == name.ToString())
            {
                return resourcePrefab;
            }
        }

        return null;
    }
}

[System.Serializable]
public class Pool
{
    public GameObject Prefab;
    public List<GameObject> PoolItems = new List<GameObject>();

    public Pool(GameObject obj)
    {
        Prefab = obj;

        GameObject go = GameObject.Instantiate(Prefab);
        go.SetActive(false);
        go.transform.name = Prefab.name;
        PoolItems.Add(go);
    }

    public GameObject GetPoolItem()
    {
        GameObject go;

        if (PoolItems.Count > 0)
        {
            go = PoolItems[0];
            PoolItems.RemoveAt(0);
        }
        else
        {
            go = GameObject.Instantiate(Prefab);
        }

        go.transform.name = Prefab.name;
        go.SetActive(true);
        return go;
    }

    public void ReturnPoolItem(GameObject obj)
    {
        obj.SetActive(false);
        PoolItems.Add(obj);
    }
}

public static class PoolExtension
{
    public static void BaseInstantiate(this GameObject prefabthis,
        [Optional] Vector3 position,
        [Optional] Quaternion rotation, Transform parent = null)
    {
        var obj = PoolManager.Instance.GetPoolItem(prefabthis);
        obj.AddComponent<PoolItem>();

        if (parent == null)
            obj.transform.SetParent(PoolManager.Instance.PoolParent);
        else obj.transform.parent = parent;

        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
    
    public static void BaseInstantiate(this PoolObjects prefabthis,
        [Optional] Vector3 position,
        [Optional] Quaternion rotation, Transform parent = null)
    {
        var obj = PoolManager.Instance.GetPoolItem(PoolManager.Instance.GetResourcePrefab(prefabthis));
        obj.AddComponent<PoolItem>();

        if (parent == null)
            obj.transform.SetParent(PoolManager.Instance.PoolParent);
        else obj.transform.parent = parent;

        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }


    // public static T BaseInstantiate<T>(this T obj) where T : MonoBehaviour
    // {
    //     T returnobj = GameObject.Instantiate(obj,PoolManager.Instance.PoolParent);
    //     return returnobj;
    // }

    // public static T BaseInstantiate<T>(this T obj, Vector3 position) where T : MonoBehaviour
    // {
    //     T returnobj = GameObject.Instantiate(obj, PoolManager.Instance.PoolParent);
    //     returnobj.transform.position = position;
    //     return returnobj;
    // }
    //
    // public static T BaseInstantiate<T>(this T obj, Vector3 position, Quaternion rotation) where T : MonoBehaviour
    // {
    //     T returnobj = GameObject.Instantiate(obj, PoolManager.Instance.PoolParent);
    //     returnobj.transform.position = position;
    //     returnobj.transform.rotation = rotation;
    //     return returnobj;
    // }

    // public static void BaseInstantiate(this Inst, GameObject prefabthis, [Optional] Transform parent,
    //     [Optional] Vector3 position,
    //     [Optional] Quaternion rotation)
    // {
    //     var obj = PoolManager.Instance.GetPoolItem(prefabthis);
    //     obj.AddComponent<PoolItem>();
    //     obj.transform.position = position;
    //     obj.transform.rotation = rotation;
    //     if (parent != null)
    //         obj.transform.SetParent(PoolManager.Instance.PoolParent);
    //     obj.transform.parent = parent;
    // }
}