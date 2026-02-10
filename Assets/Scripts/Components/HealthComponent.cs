using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    public float CurrentHealth { get { return _currentHealth; } }

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
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
