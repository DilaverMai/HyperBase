using UnityEngine;
[CreateAssetMenu(fileName = "ContactableData", menuName = "HyperBase/ContactableData", order = 0)]
public class ContactableData : ScriptableObject {
    public GameObject Prefab;
    public Vector3 Pos;
    public Quaternion Rot;
    public Vector3 Scale = Vector3.one;
    public int Value = 1;
}
