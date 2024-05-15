using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdateScript : MonoBehaviour
{
    public PlayerStats stats;

    void Start()
    {
        UpdateHealthBar(stats.Health);
    }

    void OnEnable()
    {
        stats.EventHealthChanged += UpdateHealthBar;
    }

    void OnDisable()
    {
        stats.EventHealthChanged -= UpdateHealthBar;
    }

    void UpdateHealthBar(int newHealthValue)
    {
        float _uiHealth = newHealthValue;
        _uiHealth = _uiHealth / 100 * 6;
        gameObject.transform.localScale = new Vector3(_uiHealth, 0.5f, 1);
    }
}
