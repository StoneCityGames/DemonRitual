using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public abstract class Enemy : MonoBehaviour
{
    protected HealthComponent healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public abstract void TakeDamage(float damage);
    public abstract bool IsDead();
}
