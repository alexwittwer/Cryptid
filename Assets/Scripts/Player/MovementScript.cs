using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 1f;
    private Vector2 movement;
    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().isDialogueActive || InputManager.attack || InputManager.interact)
        {
            movement.Set(0, 0);
            rb.velocity = movement;
            return;
        }
        movement.Set(InputManager.movement.x, InputManager.movement.y);

        rb.velocity = movement * speed;
        Flip();
    }

    private void Flip()
    {
        if (InputManager.movement.x > 0)
        {
            sr.flipX = true;
        }
        else if (InputManager.movement.x < 0)
        {
            sr.flipX = false;
        }
    }
}
