using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IAttack
{
    [Header("Components")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private AudioSource swordSwing;
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Attack Variables")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Vector2 _knockbackForce = new Vector2(1f, 1f);
    private float lastAttackTime = 0f;

    public int Damage { get => stats.Damage; set => stats.Damage = value; }
    [SerializeField] public BoxCollider2D Hitbox { get; set; }
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

    void Awake()
    {
        Hitbox = GetComponent<BoxCollider2D>();
        swordSwing = GetComponent<AudioSource>();
        playerAnimation = GetComponentInParent<PlayerAnimation>();
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
        if (InputManager.Attack && lastAttackTime <= 0)
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
        AudioManager.Instance.PlaySFX(swordSwing.clip);
        playerAnimation.OnAttack();
        StartCoroutine(DisableHitbox());
    }


    private IEnumerator DisableHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        Hitbox.enabled = false;
    }

    private void setOffset()
    {
        if (InputManager.LastDirection == "N")
        {
            Hitbox.offset = new Vector2(0, 0.2f);
        }
        else if (InputManager.LastDirection == "S")
        {
            Hitbox.offset = new Vector2(0, -0.2f);
        }
        else if (InputManager.LastDirection == "W")
        {
            Hitbox.offset = new Vector2(-0.2f, 0);
        }
        else if (InputManager.LastDirection == "E")
        {
            Hitbox.offset = new Vector2(0.2f, 0);
        }
    }
}
