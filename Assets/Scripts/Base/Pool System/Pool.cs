using System.Collections.Generic;
using UnityEngine;

namespace Base.PoolSystem
{
    public abstract class Pool<T>
    {
        protected Queue<T> poolList = new Queue<T>();
        [SerializeField]
        protected T _object;
        public T _Object => _object;
        public int MaxPoolSize;
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

        public abstract T CreateObject();

        public virtual T AddObject(T obj)
        {
            poolList.Enqueue(obj);

            return obj;
        }
    
    }
}