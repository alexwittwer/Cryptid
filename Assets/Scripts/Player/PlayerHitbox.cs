using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHitbox : MonoBehaviour, IDamageable

{
    public int Health { get => stats.Health; set => stats.Health += value; }
    public bool Invulnerable { get => stats.Invulnerable; set => stats.Invulnerable = value; }
    public bool Targetable { get; set; }
    public float invulnTime = 1.0f;
    public float invulnTimer = 0.0f;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private AudioSource hitSound;

    private void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

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
            AudioManager.Instance.PlaySFX(hitSound.clip, .5f);
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
            AudioManager.Instance.PlaySFX(hitSound.clip, .5f);
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
        if (stats.Health + healAmount > stats.MaxHealth)
            stats.Health = stats.MaxHealth;
        else if (healAmount > 0)
            stats.Health += healAmount;
        else return;
    }


}