using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject go;
    
    [Button]
    private void SpawnAObject()
    {
        go.BaseInstantiate();
    }
    
    [Button]
    private void SpawnAObjectWithParent(PoolObjects Ttest)
    {
        Ttest.BaseInstantiate();
    }
}
