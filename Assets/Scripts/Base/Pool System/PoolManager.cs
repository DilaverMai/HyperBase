using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Base.PoolSystem
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField]
        private List<GameObjectPool> pools = new List<GameObjectPool>();
    
        public Task Setup()
        {
            if (pools.Count > 0)
            {
                foreach (var pool in pools)
                {
                    for (int i = 0; i < pool.MaxPoolSize; i++)
                    {
                        pool.CreateObject();
                    }
                }
            }
        
            return Task.CompletedTask;
        }
    
        private GameObjectPool GetPool(ref GameObject prefab)
        {
            foreach (var pool in pools)
            {
                if (PrefabUtility.GetCorrespondingObjectFromSource(prefab) ==
                    PrefabUtility.GetCorrespondingObjectFromSource(pool._Object))
                {
                    return pool;
                }
            }
        
            var newPool = new GameObjectPool(prefab);
            pools.Add(newPool);

            return newPool;
        }    
        public void ReturnPool(GameObject prefab,Transform parent = null)
        {
            var pool = GetPool(ref prefab);
        
            if(pool != null)
            {
                if(parent)
                    prefab.transform.SetParent(parent);
            
                pool.AddObject(prefab);
            }
        }

        public GameObject Spawn(GameObject prefab,Vector3 pos = default,Vector3 rot = default,Transform parent = null)
        {
            var pool = GetPool(ref prefab);
            var spawned = pool.GetObject();

            if (pos != default)
            {
                spawned.transform.position = pos;
            }

            if (rot != default)
            {
                spawned.transform.eulerAngles = rot;
            }
        
            spawned.transform.parent = parent;
            spawned.SetActive(true);
            return spawned;
        }


    }
}
