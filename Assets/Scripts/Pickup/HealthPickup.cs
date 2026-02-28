using UnityEngine;

public class HealthPickup : AutoPickup
{
    [SerializeField] private float _heal = 25f;

    protected override bool HandleTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
        {
            if (healthComponent.CurrentHealth == healthComponent.MaxHealth)
            {
                return false;
            }
            healthComponent.SetHealth(healthComponent.CurrentHealth + _heal);
            return true;
        }
        return false;
    }
}
