using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }
    bool Invulnerable { get; set; }
    bool Targetable { get; set; }
    void OnHit(int damage, Vector2 knockback);
    void OnHit(int damage);
}