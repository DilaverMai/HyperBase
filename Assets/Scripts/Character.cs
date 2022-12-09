using System;
using UnityEngine;

public abstract class Character: MonoBehaviour
{
    [Header("Character")]
    public CharacterType Type;
    public CharacterSituation Situation;
    
    public Action OnDeath;
    public Action OnHit;
    public Action OnAttack;
}

public enum CharacterType
{
    Player,
    Friend,
    Enemy
}

public enum CharacterSituation
{
    Idle,
    Walking,
    Attacking,
    Hit,
    Dead
}