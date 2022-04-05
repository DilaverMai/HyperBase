using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractPoolObject<T> where T : Component
{
    public GameObject Prefab;
    public List<GameObject> pool = new List<GameObject>();
    public int SpawnCount = 25;
    public bool isActive = false;

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

            if (spawned.GetComponent<PoolItem>() == null)
            {
                spawned.AddComponent<PoolItem>();
            }

            pool.Add(spawned);
            spawned.SetActive(false);

        }
    }

    public T GetObject()
    {
        GameObject obj = null;

        if (pool.Count > 0)
        {
            obj = pool[0];
            pool.RemoveAt(0);
            obj.SetActive(true);
            return obj.GetComponent<T>();
        }
        else
        {
            obj = GameObject.Instantiate(Prefab);
            pool.Add(obj);
            obj.SetActive(true);
            return obj.GetComponent<T>();
        }

    }
}