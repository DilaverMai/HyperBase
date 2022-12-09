using UnityEngine;

public interface IPlayerController
{
    void MoveLeftRight(Vector2 direction);
    void Move(ref float extraSpeed);
}