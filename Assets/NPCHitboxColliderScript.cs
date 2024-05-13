using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitboxColliderScript : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC hit player");
        }

        if (other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("NPC hit by player attack");
            parent.GetComponentInChildren<EnemyHealth>().ChangeHealth(-damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC is hitting player");
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
