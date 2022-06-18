using UnityEditor;
using UnityEngine;

public class EnvironmentalControl : MonoBehaviour
{
    public LayerMask ContactMasks;
    public LayerMask GroundMasks;
    public float MaxDistance = 0.25f;
    public float Radius;
    private RaycastHit hit;
    private Vector3 spherePos;
    public Transform checkCircleTransform(Vector3 pos)
    {
        spherePos = pos;
        if (Physics.SphereCast(pos, Radius/2, transform.forward, out hit, MaxDistance, ContactMasks))
        {
            return hit.transform;
        }

        return null;
    }

    public bool CheckCircleBool(Vector3 pos)
    {
        return checkCircleTransform(pos);
    }
    
    public bool CheckGround(Vector3 pos)
    {
        return Physics.Raycast(pos, -Vector3.up, out hit, 1, GroundMasks);
    }
    
#if UNITY_EDITOR
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spherePos, Radius);
        ExtensionMethods.DrawDisc(transform.position, Radius, Color.white);
    }
#endif
}