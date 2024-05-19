using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangeMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public IAnimateSprite anim;
    public float playerOffset;
    public float speed = 0.2f;
    public float distanceMax = 2f;
    public float distanceMin = 1f;
    private bool playerInRange = false;
    private bool playerIsClose = false;

    void Start()
    {
        playerTransform = playerTransform != null ? playerTransform : GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<IAnimateSprite>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        CheckPlayerRange();

        if (playerIsClose)
        {
            MoveAway();
            anim.OnMove();
        }
        else if (playerInRange)
        {
            Move();
            anim.OnMove();
        }
        else
        {
            anim.OnIdle();
        }
    }

    private void CheckPlayerRange()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance < distanceMax)
            playerInRange = true;
        else
            playerInRange = false;

        if (distance < distanceMin)
            playerIsClose = true;
        else
            playerIsClose = false;
    }

    void Flip()
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

    void Move()
    {
        float step = speed * Time.deltaTime;
        Vector3 playerWithOffset = playerTransform.position + new Vector3(0, playerOffset, 0);
        rb.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
    }

    void MoveAway()
    {
        float step = speed * Time.deltaTime;
        Vector3 playerWithOffset = playerTransform.position - new Vector3(0, playerOffset, 0);
        rb.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
    }
}
