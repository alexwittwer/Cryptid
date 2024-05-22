using UnityEngine;

public interface IAnimateSprite
{
    void ChangeAnimationState(int newState);
    void OnHit();
    void OnAttack();
    void OnMove();
    void OnIdle();
    void OnDeath();
    void OnWake();

}