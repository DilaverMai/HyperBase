using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Action OnHit;
    public Action OnDeath;
    public Action OnAttack;

    //Unity Functions
    public Action OnStart;
    public Action OnUpdate;

    //Touch Functions
    public Action OnFirstTouch;
    public Action OnTouchDown;
    public Action OnTouchUp;

    public CharacterStat _CharacterStat = CharacterStat.live;
    public CharacterAnimation _CharacterAnimation;
    public CharacterAI _CharacterAI;

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
}

public enum CharacterStat
{
    live,
    death
}