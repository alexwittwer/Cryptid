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
    public float distanceMax = .4f;
    private bool playerInDistance = false;

    void Start()
    {
        playerHitbox = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInRange();
    }

    void FixedUpdate()
    {
        if (playerInDistance)
        {
            MoveTowardsPlayerLogic();
        }
    }

    private void MoveTowardsPlayerLogic()
    {
        float step = speed * Time.deltaTime;
        Vector3 playerWithOffset = playerTransform.position + new Vector3(0, playerOffset, 0);
        transform.position = Vector2.MoveTowards(transform.position, playerWithOffset, step);
    }

    private void CheckIfInRange()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        playerInDistance = distance < distanceMax;
    }
}
