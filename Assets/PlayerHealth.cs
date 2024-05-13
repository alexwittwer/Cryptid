using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityTimeSeconds = 1f;
    [SerializeField] private float invulnerabilityDeltaTime = 0.1f;
    [SerializeField] public int CurrentHealth => currentHealth;
    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There is already an instance of PlayerStats in the scene");
        }
    }

    void Start()
    {
        currentHealth = 6;
    }

    void Update()
    {

        if (currentHealth <= 0)
        {
        }

    }


    public void ChangeHealth(int value)
    {
        ChangeHealthPrivate(value);
    }

    private void ChangeHealthPrivate(int value)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth += value;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return;
        }

        StartCoroutine(temporaryInvulnerability());

    }

    private IEnumerator temporaryInvulnerability()
    {
        Debug.Log("Player invuln");
        isInvulnerable = true;

        for (float i = 0; i < invulnerabilityTimeSeconds; i += invulnerabilityDeltaTime)
        {
            yield return new WaitForSeconds(invulnerabilityDeltaTime);
        }

        isInvulnerable = false;
        Debug.Log("Player not invuln");
    }
}
