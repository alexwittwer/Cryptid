using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _activeHealth;
    [SerializeField] private bool _isInvulnerable = false;

    public int Health
    {
        get
        {
            return _activeHealth;
        }
        set
        {
            _activeHealth = Math.Max(value, 0);
            HealthChanged?.Invoke(_activeHealth);
        }
    }

    public bool invuln
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

    public UnityAction<int> HealthChanged;

    public void InitStats()
    {
        _health = _maxHealth;
        _activeHealth = _health;
    }
}
