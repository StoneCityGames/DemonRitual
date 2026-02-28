using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.gameObject.layer)) <= 0)
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out HealthComponent healthComponent))
        {
            healthComponent.SetHealth(healthComponent.CurrentHealth - _damage);
        }
    }
}
