using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    void Awake()
    {
        visualCue.SetActive(false);
        playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().isDialogueActive)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Trigger Dialogue
                DialogueManager.GetInstance().EnterDialogue(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }




}
