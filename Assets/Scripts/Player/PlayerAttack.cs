using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IAttack
{

    [SerializeField] public BoxCollider2D Hitbox { get; set; }
    [SerializeField] private PlayerStats stats;
    public int Damage { get => stats.Damage; set => stats.Damage = value; }
    public Vector2 KnockbackForce
    {
        get
        {
            return _knockbackForce;
        }
        set
        {
            _knockbackForce = new Vector2(value.x, value.y);
        }
    }
    private float attackCooldown = 0.5f;
    [SerializeField] private Vector2 _knockbackForce = new Vector2(1f, 1f);
    private float lastAttackTime = 0f;


    void Awake()
    {
        Hitbox = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        Damage = stats.Damage;
        Hitbox.enabled = false;
        KnockbackForce = new Vector2(1f, 1f);
    }

    void Update()
    {
        setOffset();
        if (InputManager.attack && lastAttackTime <= 0)
        {
            lastAttackTime = attackCooldown;
            Attack();
        }
        else
        {
            lastAttackTime -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        Hitbox.enabled = true;
        StartCoroutine(DisableHitbox());
    }


    private IEnumerator DisableHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        Hitbox.enabled = false;
    }

    private void setOffset()
    {
        if (InputManager.movement.x > 0)
        {
            Hitbox.offset = new Vector2(0.25f, 0);
        }
        else if (InputManager.movement.x < 0)
        {
            Hitbox.offset = new Vector2(-0.25f, 0);
        }
        else if (InputManager.movement.y > 0)
        {
            Hitbox.offset = new Vector2(0, 0.25f);
        }
        else if (InputManager.movement.y < 0)
        {
            Hitbox.offset = new Vector2(0, -0.25f);
        }
    }
}
