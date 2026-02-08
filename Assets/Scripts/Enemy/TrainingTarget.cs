using UnityEngine;

public class TrainingTarget : Enemy
{
    [SerializeField, Tooltip("Respawn time in seconds")] private float _respawnTime = 1f;
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth = 100f;

    private void Awake()
    {
        ResetHealth();
    }

    public override bool IsDead()
    {
        return _currentHealth <= 0f;
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(_currentHealth - damage, 0f);
        if (IsDead())
        {
            Die();
            Invoke(nameof(Respawn), _respawnTime);
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        ResetHealth();
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
