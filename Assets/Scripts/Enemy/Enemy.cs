using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public abstract class Enemy : MonoBehaviour
{
    protected HealthComponent healthComponent;

    protected void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public abstract void TakeDamage(float damage, Vector3 direction, bool shouldDismember);
    public abstract bool IsDead();
    public abstract bool IsColliderVisibleFrom(Vector3 point, LayerMask obstacleMask);
}
