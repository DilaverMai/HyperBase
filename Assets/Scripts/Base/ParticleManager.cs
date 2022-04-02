using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    
}

public class PoolParticle : PoolObject<Enum_PoolObject,ParticleItem>
{
    public PoolParticle(GameObject gameObject) : base(gameObject)
    {
    }


}
