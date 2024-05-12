using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth = 6;
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


    public void ChangeHealth(int value)
    {
        currentHealth += value;
        Debug.Log("Player health: " + currentHealth);
    }
}
