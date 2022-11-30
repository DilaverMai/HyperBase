using UnityEngine;

namespace Base.PoolSystem
{
    public static class ExtensionGameObjectPools{
    
        public static GameObject Spawn(this GameObject prefab,Vector3 pos = default,Vector3 rot = default,Transform parent = null)
        {
            return PoolManager.Instance.Spawn(prefab,pos,rot,parent);
        }

        public static void ReturnPool(this GameObject obj)
        {
            PoolManager.Instance.ReturnPool(obj);
        }
    
        public static GameObject ResetRigibody(this GameObject obj)
        {
            var rigidbody = obj.GetComponent<Rigidbody>();
        
            if (!rigidbody)
            {
                Debug.LogWarning("Object Haven't a rigidbody");
                return obj;
            }
        
            rigidbody.velocity = Vector3.zero;
            return obj;
        }
    
        public static GameObject ResetPosition(this GameObject obj)
        {
            obj.transform.position = Vector3.zero;
            return obj;
        }

        public static GameObject ResetRotation(this GameObject obj)
        {
            obj.transform.eulerAngles = Vector3.zero;
            return obj;
        }
    
        public static GameObject ResetScale(this GameObject obj)
        {
            obj.transform.localScale = Vector3.one;
            return obj;
        }
    
    }
}