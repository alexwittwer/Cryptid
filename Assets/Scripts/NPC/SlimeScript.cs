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
    [SerializeField] private bool _invulnerable = false;
    [SerializeField] private int _health = 1;
    [SerializeField] private bool _targetable = true;
    [SerializeField] private int Damage = 1;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float InvulnerableTime = 1f;


    [Header("Animator Variables")]
    [SerializeField] private string currentState;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isHurt;
    [SerializeField] private bool isDead;


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

    void Start()
    {
        ChangeAnimationState("Idle");
    }

    private void Update()
    {
        CheckIfInRange();
        Flip();

        if (Invulnerable)
        {
            timer += Time.deltaTime;
            if (timer >= InvulnerableTime)
            {
                Invulnerable = false;
                timer = 0f;
            }
        }

        if (playerInRange)
        {
            Move();
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
        if (Invulnerable) return;
        Health -= damage;
        Invulnerable = true;
        ChangeAnimationState("Hurt");
        if (Health <= 0)
        {
            OnDeath();
            OnObjectDestroyed(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void OnHit(int damage)
    {
        if (Invulnerable) return;
        Health -= damage;
        Invulnerable = true;
        ChangeAnimationState("Hurt");
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        ChangeAnimationState("Death");
    }

    public void OnObjectDestroyed(float time)
    {
        Destroy(gameObject, time);
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

    private void OnMove()
    {
        ChangeAnimationState("Move");
    }

    private void OnIdle()
    {
        ChangeAnimationState("Idle");
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

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        animator.CrossFade(newState, 0.1f, 0);

        currentState = newState;
    }
}
