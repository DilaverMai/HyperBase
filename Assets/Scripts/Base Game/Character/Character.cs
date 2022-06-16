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
    public Action OnAwake;

    //Touch Functions
    public CharacterStat _CharacterStat = CharacterStat.live;
    [HideInInspector] public CharacterAnimation _CharacterAnimation;
    [HideInInspector] public CharacterAI _CharacterAI;

    protected virtual void Awake()
    {
        _CharacterAnimation = GetComponent<CharacterAnimation>();
        _CharacterAI = GetComponent<CharacterAI>();
        OnAwake?.Invoke();
    }

    protected virtual void Start()
    {
        OnStart?.Invoke();
    }

    private void Update()
    {
        if(!Base.IsPlaying()) return;
        OnUpdate?.Invoke();
    }
}

public enum CharacterStat
{
    live,
    death
}