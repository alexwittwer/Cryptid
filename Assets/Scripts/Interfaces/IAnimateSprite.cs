using UnityEngine;

public interface IAnimateSprite
{
    public string HURT { get; }
    public string IDLE { get; }
    public string MOVE { get; }
    public string ATTACK { get; }
    void ChangeAnimationState(string newState);
    void OnHit();
    string GetCurrentState();
}