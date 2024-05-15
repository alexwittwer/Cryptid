using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitbox : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool invulnerable = false;
    [SerializeField] private bool targetable = true;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject parent;
    public int Health { get => health; set => health = value; }
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }
    public bool Targetable { get => targetable; set => targetable = value; }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (knockback != Vector2.zero)
        {
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        }

        if (!invulnerable)
        {
            health -= damage;
            if (health <= 0)
            {
                OnObjectDestroyed();
            }
        }
    }

    public void OnHit(int damage)
    {
        if (!invulnerable)
        {
            health -= damage;
            if (health <= 0)
            {
                OnObjectDestroyed();
            }
        }
    }

    public void OnObjectDestroyed()
    {
        Destroy(parent);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(damage);
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            OnHit(other.GetComponent<IAttack>().Damage, other.GetComponent<IAttack>().KnockbackForce);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(damage);
        }
    }
}