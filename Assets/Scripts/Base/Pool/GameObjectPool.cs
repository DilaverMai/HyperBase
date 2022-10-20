using System;
using UnityEngine;

[Serializable]
public class GameObjectPool : Pool<GameObject>
{
    protected override GameObject CreateObject()
    {
        var obj = GameObject.Instantiate(_object);
        obj.SetActive(false);
        poolList.Enqueue(obj);

        return obj;
    }

    public override GameObject AddObject(GameObject obj)
    {
        base.AddObject(obj);
        obj.SetActive(false);

        return obj;
    }

    public GameObjectPool(GameObject obj) : base(obj)
    {
        
    }
}