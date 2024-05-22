using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeScript : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform slime;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHitbox playerHitbox;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer sr;

    [Header("Movement Variables")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distanceMax = 1f;
    [SerializeField] private float playerOffset = 1f;
    [SerializeField] private bool playerInRange = false;
    [SerializeField] private bool CanMove = true;

    [Header("Status Variables")]
    [SerializeField] private bool isAwake = false;
    [SerializeField] private bool _invulnerable = false;
    [SerializeField] private int _health = 1;
    [SerializeField] private bool _targetable = true;
    [SerializeField] private int Damage = 1;

    public bool Invulnerable { get => _invulnerable; set => _invulnerable = value; }
    public bool Targetable { get => _targetable; set => _targetable = value; }
    public int Health { get => _health; set => _health = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slime = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        playerHitbox = player.GetComponentInChildren<PlayerHitbox>();
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!CanMove)
        {
            agent.velocity = Vector3.zero;
            return;
        }
        CheckIfInRange();

        Flip();

        if (playerInRange)
        {
            Move();
            if (!isAwake)
            {
                isAwake = true;
            }
        }
        else
        {
            agent.velocity = Vector3.zero;
            OnIdle();
        }
    }


    private void Move()
    {
        agent.SetDestination(player.transform.position);
        OnMove();
    }

    private void CheckIfInRange()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < distanceMax)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    private void Flip()
    {
        if (player.transform.position.x < transform.position.x && !sr.flipX)
        {
            sr.flipX = true;
        }
        else if (player.transform.position.x > transform.position.x && sr.flipX)
        {
            sr.flipX = false;
        }
    }

    private int GetDamage(Collider2D other)
    {
        int _damage = other.GetComponent<IAttack>().Damage;
        return _damage;
    }

    private Vector2 GetKnockbackDirection(Collider2D other)
    {
        Vector2 _kbOther = other.GetComponent<IAttack>().KnockbackForce;
        Vector2 _kb = new Vector2(Mathf.Sign(transform.position.x - other.transform.position.x) * _kbOther.x, Mathf.Sign(transform.position.y - other.transform.position.y) * _kbOther.y).normalized;

        return _kb;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDeath();
        }
        else
        {
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    private void OnDeath()
    {
        animator.SetTrigger("Death");
        CanMove = false;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            OnObjectDestroyed();
        }
    }

    private void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnMove()
    {
        animator.SetBool("isMoving", true);
    }

    private void OnIdle()
    {
        animator.SetBool("isMoving", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(Damage);
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            Vector2 _kb = GetKnockbackDirection(other);
            int _damage = GetDamage(other);

            OnHit(_damage, _kb);
        }
    }

    public void OnHit(int damage)
    {
        throw new System.NotImplementedException();
    }

    void IDamageable.OnObjectDestroyed()
    {
        throw new System.NotImplementedException();
    }
}
