using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private int damage = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            damage = other.gameObject.GetComponent<NPCHitboxColliderScript>().GetDamage();
            PlayerHealth.instance.ChangeHealth(-damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            damage = other.gameObject.GetComponent<NPCHitboxColliderScript>().GetDamage();
            PlayerHealth.instance.ChangeHealth(-damage);
        }
    }
}
