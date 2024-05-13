using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTowardsPlayer : MonoBehaviour
{
    public BoxCollider2D playerHitbox;
    public float speed = 0.2f;
    public float distanceMax - 10f;
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
        float offsetY = playerHitbox.GetComponent<BoxCollider2D>().offset.y;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerHitbox.transform.position.x, playerHitbox.transform.position.y + offsetY), step);
    }

    private void CheckIfInRange()
    {
        float currX;
        float currY;
        float playerX;
        float playerY;

        float distance = Mathf.sqrt(Mathf.pow((playerX - currX), 2) + Mathf.pow((playerY - currY), 2));

        if (distance < distanceMax)
        {
            playerInDistance = true;
        } else
        {
            playerInDistance = false;
        }
        
    }
}
