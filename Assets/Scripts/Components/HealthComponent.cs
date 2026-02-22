using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public event Action<float> OnHealthChanged;
    public float CurrentHealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }

    private float _currentHealth;

    private void Awake()
    {
        ResetHealth();
    }

    public bool IsAlive()
    {
        return _currentHealth > 0f;
    }

    public void ResetHealth()
    {
        SetHealth(_maxHealth);
    }

    public void SetHealth(float health)
    {
        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
    }
}
