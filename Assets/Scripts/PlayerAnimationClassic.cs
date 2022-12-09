using System;
using UnityEngine;

public class PlayerAnimationClassic : MonoBehaviour, IPlayerAnimation
{
    private Animator playerAnimator;
    private Vector3 lastPosition;
    private Character baseCharacter;
    private void Awake()
    {
        baseCharacter = GetComponent<Character>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        baseCharacter.OnDeath += OnDeath;
        baseCharacter.OnHit += OnHit;
    }
    
    private void OnDisable()
    {
        baseCharacter.OnDeath -= OnDeath;
        baseCharacter.OnHit -= OnHit;
    }

    public void SetSpeed()
    {
        var speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        playerAnimator.SetFloat("Speed", speed);
    }

    public void Jump()
    {
        playerAnimator.SetTrigger("Jump");
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
    }

    public void OnDeath()
    {
        playerAnimator.SetTrigger("Death");
    }

    public void OnHit()
    {
        playerAnimator.SetTrigger("Hit");
    }
}