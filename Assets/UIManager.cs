using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject healthBarFilled;

    void Awake()
    {
        UpdateHealthBar(stats.Health);
    }

    void OnEnable()
    {
        stats.HealthChanged += UpdateHealthBar;
    }

    void OnDisable()
    {
        stats.HealthChanged -= UpdateHealthBar;
    }

    void UpdateHealthBar(int newHealthValue)
    {
        float _uiHealth = newHealthValue;
        _uiHealth = _uiHealth / 100 * 6;
        healthBarFilled.GetComponent<RectTransform>().localScale = new Vector3(_uiHealth, 0.5f, 1);
    }

}
