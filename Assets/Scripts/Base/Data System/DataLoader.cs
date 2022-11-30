using UnityEngine;

namespace Base.DataSystem
{
    public abstract class DataLoader<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Current => Resources.Load<T>("Settings/"+typeof(T).Name);
    }
}

