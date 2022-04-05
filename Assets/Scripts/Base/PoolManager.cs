using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<PoolObject>
    PoolObjects = new List <PoolObject>();
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

    private void Start()
    {
        foreach (var item in PoolObjects)
        {
            item.Setup();
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
                item.GetObject().gameObject.SetActive(true);
                return item.GetObject();
            }
        }

        return null;
    }


}

 [System.Serializable]
 public class PoolObject: AbstractPoolObject<PoolItem>
 {
    public Enum_PoolObject Enum;
 }   
