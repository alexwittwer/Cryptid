using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 50f;
    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public SpriteRenderer sr;
    private Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {

        if (DialogueManager.GetInstance().isDialogueActive)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        // Movement
        rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, direction.y * speed * Time.fixedDeltaTime);
        direction.Normalize();
    }
}
