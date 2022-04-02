using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "HyperBase/PlayerData", order = 0)]
public class PlayerData : Settings<PlayerData>
{
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed => moveSpeed;


    [SerializeField]
    private bool yMoving = false;
    public bool YMoving => yMoving;
    [SerializeField]
    private bool xMoving = true;
    public bool XMoving => xMoving;
    [SerializeField]
    private bool zMoving = false;
    public bool ZMoving => zMoving;

    [SerializeField]
    private Vector2 clampX;
    public Vector2 ClampX => clampX;
    [SerializeField]
    private Vector2 clampY;
    public Vector2 ClampY => clampY;
}