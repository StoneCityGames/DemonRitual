using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TrainingTarget : Enemy
{
    [SerializeField, Tooltip("Respawn time in seconds")] private float _respawnTime = 1f;

    public override bool IsDead()
    {
        return !healthComponent.IsAlive();
    }

    public override void TakeDamage(float damage)
    {
        healthComponent.SetHealth(healthComponent.CurrentHealth - damage);
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
        healthComponent.ResetHealth();
    }
}
