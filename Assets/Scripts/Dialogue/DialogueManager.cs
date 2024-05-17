using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Manager")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;
    private static DialogueManager instance;// Singleton instance
    private Story currentStory;
    public bool isDialogueActive { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists!");
        }
    }

    void OnEnable()
    {
        InputManager.interactAction += ContinueStory;
    }

    void OnDisable()
    {
        InputManager.interactAction -= ContinueStory;
    }


    private void Start()
    {
        isDialogueActive = false;
        continueIcon.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {

        if (!isDialogueActive)
        {
            return;
        }

        if (currentStory)
        {
            if (currentStory.canContinue)
            {
                continueIcon.SetActive(true);
            }
            else
            {
                continueIcon.SetActive(false);
            }
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        // Create a new Story object with the loaded ink JSON text
        currentStory = new Story(inkJSON.text);
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        ContinueStory(true);
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        dialogueText.text = null;
        currentStory = null;
    }

    public void ContinueStory(bool value)
    {
        if (value && isDialogueActive)
        {
            if (currentStory && currentStory.canContinue)
            {
                string text = currentStory.Continue();
                dialogueText.text = text;
            }
            else
            {
                StartCoroutine(ExitDialogue());
            }
        }
    }

}