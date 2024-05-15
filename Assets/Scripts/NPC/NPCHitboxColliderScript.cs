using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitboxColliderScript : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.gameObject.GetComponentInParent<PlayerCharacter>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            parent.GetComponentInChildren<EnemyHealth>().ChangeHealth(-damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.gameObject.GetComponentInParent<PlayerCharacter>().TakeDamage(damage);
        }
    }
}
