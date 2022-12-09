using UnityEngine;

public class Player : Character
{
    private IPlayerAnimation IplayerAnimation;
    private IPlayerController IplayerController;
    private IPlayerHealthSystem IplayerHealthSystem;
    
    [Header("Player Settings")]
    public float ExtraSpeed;
    
    private void Awake()
    {
        IplayerAnimation = GetComponent<IPlayerAnimation>();
        IplayerController = GetComponent<IPlayerController>();
        IplayerHealthSystem = GetComponent<IPlayerHealthSystem>();
    }
    
    private void FixedUpdate()
    {
        if (IplayerHealthSystem.IsDead())
            return;
        
        IplayerController.Move(ref ExtraSpeed);
        IplayerAnimation.SetSpeed();
    }
}