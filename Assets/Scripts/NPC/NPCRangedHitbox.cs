using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCRangedHitbox : MonoBehaviour, IDamageable
{
    [Header("Status Variables")]
    [SerializeField] private int health = 3;
    [SerializeField] private bool invulnerable = false;
    [SerializeField] private bool targetable = true;
    [SerializeField] private int damage = 1;
    [SerializeField] private float invulnerableTime = 0.5f;
    [SerializeField] private float lastHitTime = 0f;

    [Header("Components")]
    [SerializeField] private AnimatorBrain animateSprite;
    [SerializeField] private NPCRangedMovement movement;
    [SerializeField] private CameraShake vcam;

    public int Health { get => health; set => health = value; }
    public bool Invulnerable { get => invulnerable; set => invulnerable = value; }
    public bool Targetable { get => targetable; set => targetable = value; }

    private void Awake()
    {
        animateSprite = GetComponentInParent<AnimatorBrain>();
        movement = GetComponentInParent<NPCRangedMovement>();
        vcam = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            movement.Immobilize();
            animateSprite.OnDeath();
        }

        if (invulnerable && lastHitTime > 0)
        {
            lastHitTime -= Time.deltaTime;
        }
        else
        {
            invulnerable = false;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (!invulnerable)
        {
            health -= damage;
            invulnerable = true;
            lastHitTime = invulnerableTime;

            if (health <= 0)
            {
                movement.Immobilize();
                animateSprite.OnDeath();
            }
            else
            {
                animateSprite.OnHit();
            }
        }
    }

    public void OnHit(int damage)
    {
        OnHit(damage, Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(damage);
        }

        if (other.CompareTag("Attack"))
        {
            Vector2 kb = GetKnockbackDirection(other);
            int dmg = GetDamage(other);
            vcam.StartMiniShake();
            OnHit(dmg, kb);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(damage);
        }
    }

    private Vector2 GetKnockbackDirection(Collider2D other)
    {
        Vector2 kbOther = other.GetComponent<IAttack>().KnockbackForce;
        Vector2 kb = new Vector2(Mathf.Sign(transform.position.x - other.transform.position.x) * kbOther.x, Mathf.Sign(transform.position.y - other.transform.position.y) * kbOther.y).normalized;
        return kb;
    }

    private int GetDamage(Collider2D other)
    {
        return other.GetComponent<IAttack>().Damage;
    }
}
