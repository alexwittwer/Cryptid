using UnityEngine;

public interface IProjectile
{
    float Speed { get; set; }
    int Damage { get; set; }
    void OnHit();
    void OnObjectDestroyed();
}