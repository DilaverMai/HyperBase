using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T>
{
    protected Queue<T> poolList = new Queue<T>();
    [SerializeField]
    protected T _object;
    public T _Object => _object;
    
    public Pool(T obj)
    {
        _object = obj;
    }
    
    public virtual T GetObject()
    {
        if (poolList.Count == 0)
            CreateObject();
    
        return poolList.Dequeue();
    }

    protected abstract T CreateObject();

    public virtual T AddObject(T obj)
    {
        poolList.Enqueue(obj);

        return obj;
    }
    
}