using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{

    [Header("Attack Variables")]
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private float damage = 1f;
    private float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;


    void Start()
    {
        hitbox.enabled = false;
    }
    void Update()
    {
        Flip();
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
        hitbox.enabled = true;
        StartCoroutine(DisableHitbox());
    }

    void Flip()
    {
        if (InputManager.movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (InputManager.movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator DisableHitbox()
    {
        yield return new WaitForSeconds(0.2f);
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Player hit enemy");
            collision.GetComponent<EnemyHealth>().ChangeHealth(-damage);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
