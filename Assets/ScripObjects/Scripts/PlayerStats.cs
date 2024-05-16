using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents the stats of the player character.
/// </summary>
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats : ScriptableObject
{
<<<<<<< main

<<<<<<< HEAD
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;
=======
    [SerializeField] private int _health = 12;
    [SerializeField] private int _maxHealth = 12;
    [SerializeField] private int _damage = 1;
>>>>>>> local
=======
    [SerializeField] private int _health = 12;
    [SerializeField] private int _maxHealth = 12;
    [SerializeField] private int _damage = 1;
>>>>>>> c7b1392d99b9dfad940947a33bf3776b669126f8
    [SerializeField] private int _activeHealth;
    [SerializeField] private bool _isInvulnerable = false;

    /// <summary>
    /// The current health of the player.
    /// </summary>
    public int Health
    {
        get
        {
            return _activeHealth;
        }
        set
        {
            // ensures health does not go below 0
            _activeHealth = Math.Max(value, 0);
            EventHealthChanged?.Invoke(_activeHealth);
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = Math.Max(value, 0);
            EventMaxHealthChanged?.Invoke(_maxHealth);
        }
    }

    /// <summary>
    /// Determines if the player is invulnerable.
    /// </summary>
    public bool Invulnerable
    {
        get
        {
            return _isInvulnerable;
        }
        set
        {
            _isInvulnerable = value;
        }
    }

<<<<<<< HEAD
<<<<<<< main
=======
    /// <summary>
    /// The damage dealt by the player.
    /// </summary>
=======
>>>>>>> c7b1392d99b9dfad940947a33bf3776b669126f8
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
<<<<<<< HEAD
=======

    public UnityAction<int> EventHealthChanged;
>>>>>>> c7b1392d99b9dfad940947a33bf3776b669126f8

    /// <summary>
    /// Event that is triggered when the player's health changes.
    /// </summary>
>>>>>>> local
    public UnityAction<int> EventHealthChanged;
    public UnityAction<int> EventMaxHealthChanged;

    /// <summary>
    /// Initializes the player's stats.
    /// </summary>
    public void InitStats()
    {
        _health = _maxHealth;
        _activeHealth = _health;
    }
}
