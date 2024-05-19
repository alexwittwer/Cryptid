using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private TextAsset inkJSON;
    private Rigidbody2D playerRigidbody;

    private bool playerInRange;

    void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().isDialogueActive)
        {
            visualCue.SetActive(true);
            if (InputManager.Interact)
            {
                playerRigidbody.velocity = Vector2.zero;
                EnterDialogue(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            playerInRange = false;
        }
    }

    void EnterDialogue(TextAsset inkJSON)
    {
        DialogueManager.GetInstance().EnterDialogue(inkJSON);
    }
}