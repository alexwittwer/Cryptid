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
        CheckIfInRange();

        if (playerTransform.position.x < transform.position.x && !sr.flipX)
        {
            sr.flipX = true;
        }
        else if (playerTransform.position.x > transform.position.x && sr.flipX)
        {
            sr.flipX = false;
        }
    }

    void FixedUpdate()
    {
        if (playerInRange)
        {
            float step = speed * Time.deltaTime;
            Vector3 playerWithOffset = playerTransform.position + new Vector3(0, playerOffset, 0);
            rb.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
            anim.OnMove();
        }
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

    private void CheckIfPlayerClose()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if (distance < distanceMin)
        {
            playerIsClose = true;
        }
        else
        {
            playerIsClose = false;
        }
    }
}
