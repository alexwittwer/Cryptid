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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().isDialogueActive)
        {
            return;
        }
        movement.Set(InputManager.movement.x, InputManager.movement.y);

        rb.velocity = movement * speed;
    }
}