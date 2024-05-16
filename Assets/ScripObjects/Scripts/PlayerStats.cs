using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents the stats of the player character.
/// </summary>
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int _health = 12;
    [SerializeField] private int _maxHealth = 12;
    [SerializeField] private int _damage = 1;
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
            if (_activeHealth > value)
            {
                EventHealthDamaged?.Invoke(_activeHealth - value); // this is damage taken
            }
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

    /// <summary>
    /// The damage dealt by the player.
    /// </summary>
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

    /// <summary>
    /// Event that is triggered when the player's health changes.
    /// </summary>
    public UnityAction<int> EventHealthChanged;
    public UnityAction<int> EventHealthDamaged;
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
