using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour, IProjectile
{
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public static int _damage = 1;
    public static float _speed = 2f;

    private float timer = 0f;
    private float maxTimer = 5f;

    public void OnHit()
    {
        OnObjectDestroyed();
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (timer >= maxTimer)
        {
            OnObjectDestroyed();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.GetComponent<IDamageable>().OnHit(_damage);
            OnHit();
        }
    }
}
