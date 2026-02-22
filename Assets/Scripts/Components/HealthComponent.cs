using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public event Action<float> OnHealthChanged;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    public float CurrentHealth { get { return _currentHealth; } }
    public float MaxHealth { get { return _maxHealth; } }

    private void Awake()
    {
        ResetHealth();
    }

    public bool IsAlive()
    {
        return _currentHealth > 0f;
    }

    public void SetHealth(float health)
    {
        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
