using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 0.2f;
    public float distanceMax = 10f;
    private bool playerInDistance = false;
    
    void Start()
    {
        playerHitbox = GameObject.Find("Player").GetComponent<BoxCollider2D>();
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
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, step);
    }

    private void CheckIfInRange()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        playerInDistance = distance < distanceMax;
    }
}
