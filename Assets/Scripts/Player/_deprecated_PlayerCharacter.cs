using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerStats stats;
    public float invulnTime = 1.0f;
    public float invulnTimer = 0.0f;
    public bool invuln = false;


    void Update()
    {
        if (invulnTimer > 0)
        {
            invuln = true;
            invulnTimer -= Time.deltaTime;
        }
        else
        {
            invuln = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (invuln || invulnTimer > 0)
        {
            return;
        }
        else
        {
            invulnTimer = invulnTime;
            stats.Health -= damage;
            invuln = true;
        }
    }

    public void Heal(int healAmount)
    {
        stats.Health += healAmount;
    }
}
