using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHitbox : MonoBehaviour, IDamageable

{
    public int Health { get => stats.Health; set => stats.Health += value; }
<<<<<<< HEAD
    public bool Invulnerable { get => stats.Invulnerable; set => stats.Invulnerable = value; }
=======
    public bool Invulnerable { get => stats.invuln; set => stats.invuln = value; }
>>>>>>> c7b1392d99b9dfad940947a33bf3776b669126f8
    public bool Targetable { get; set; }
    public float invulnTime = 1.0f;
    public float invulnTimer = 0.0f;
    [SerializeField] private PlayerStats stats;

    public void Update()
    {
        if (invulnTimer > 0)
        {
            Invulnerable = true;
            invulnTimer -= Time.deltaTime;
        }
        else
        {
            Invulnerable = false;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (Invulnerable || invulnTimer > 0)
        {
            return;
        }
        else
        {
            invulnTimer = invulnTime;
            stats.Health -= damage;
            Invulnerable = true;
        }

        if (knockback != Vector2.zero)
        {
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void OnHit(int damage)
    {
        if (Invulnerable || invulnTimer > 0)
        {
            return;
        }
        else
        {
            invulnTimer = invulnTime;
            stats.Health -= damage;
            Invulnerable = true;
        }
    }

    public void OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }

    public void Heal(int healAmount)
    {
<<<<<<< HEAD
        if (stats.Health + healAmount > stats.MaxHealth)
            stats.Health = stats.MaxHealth;
        else if (healAmount > 0)
            stats.Health += healAmount;
        else return;
=======
        if (healAmount > 0)
            stats.Health += healAmount;
>>>>>>> c7b1392d99b9dfad940947a33bf3776b669126f8
    }


}