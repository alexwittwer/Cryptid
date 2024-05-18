using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public IAnimateSprite anim;
    public float playerOffset;
    public float speed = 1f;
    public float distanceMax = .1f;
    private bool playerInRange = false;

    void Start()
    {
        playerTransform = playerTransform != null ? playerTransform : GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<IAnimateSprite>();
    }

    void Update()
    {
        CheckIfInRange();

        Flip();

        if (playerInRange)
        {
            Move();
            anim.OnMove();
        }
        else
        {
            anim.OnIdle();
        }
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        Vector3 playerWithOffset = playerTransform.position + new Vector3(0, playerOffset, 0);
        rb.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
    }

    private void CheckIfInRange()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance < distanceMax)
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
        if (playerTransform.position.x < transform.position.x && !sr.flipX)
        {
            sr.flipX = true;
        }
        else if (playerTransform.position.x > transform.position.x && sr.flipX)
        {
            sr.flipX = false;
        }
    }
}
