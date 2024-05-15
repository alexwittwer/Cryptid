using UnityEngine;

public interface IAttack
{
    Vector2 KnockbackForce { get; set; }
    int Damage { get; set; }
    BoxCollider2D Hitbox { get; set; }

}