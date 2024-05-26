using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AudioSource audioSource;

    private bool canMeow = false;
    private float time = 0f;
    private float meowTime = 1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sr.enabled = false;
    }

    private void Update()
    {
        if (InputManager.Interact && time >= meowTime)
        {
            time = 0;
            if (canMeow) AudioManager.Instance.PlaySFX(audioSource.clip, .6f);
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sr.enabled = false;
            canMeow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sr.enabled = true;
            canMeow = true;
        }
    }

    private void PlayMeow()
    {

    }
}
