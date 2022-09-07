using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private void OnDisable()
    {
        PoolManager.Instance.ReturnPoolItem(gameObject);
    }
}
