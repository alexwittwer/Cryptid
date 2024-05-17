using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTowardsPlayer : MonoBehaviour
{
    public BoxCollider2D playerHitbox;
    public Transform playerTransform;
    public float playerOffset;
    public float speed = 0.2f;
    public float distanceMax = .1f;
    private bool playerInRange = false;

    void Start()
    {
        playerTransform = playerTransform != null ? playerTransform : GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInRange();
    }

    void FixedUpdate()
    {
        if (playerInRange)
        {
            float step = speed * Time.deltaTime;
            Vector3 playerWithOffset = playerTransform.position + new Vector3(0, playerOffset, 0);
            transform.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
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
}
