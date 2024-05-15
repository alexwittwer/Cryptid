using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public interface IDialogueManager
{
    void EnterDialogue(TextAsset inkJSON);
    void ContinueStory();
}

public interface IDialogueDisplay
{
    void ShowDialogue(string text);
    void HideDialogue();
}

public interface IInputProvider
{
    bool GetKeyDown(KeyCode keyCode);
}

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator routine);
}

public class DialogueManager : MonoBehaviour, IDialogueManager
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

    private void Start()
    {
        isDialogueActive = false;
        continueIcon.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
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

            if (InputManager.interact)
            {
                ContinueStory();
            }
        }

        if (!isDialogueActive)
        {
            return;
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
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        dialogueText.text = null;
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
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
