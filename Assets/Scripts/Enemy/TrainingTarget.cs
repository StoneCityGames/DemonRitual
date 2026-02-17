using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(LimbSpawner))]
public class TrainingTarget : Enemy
{
    [SerializeField, Tooltip("Respawn time in seconds")] private float _respawnTime = 1f;

    private CapsuleCollider _capsuleCollider;
    private LimbSpawner _limbSpawner;

    private void OnDrawGizmos()
    {
        if (!_capsuleCollider)
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        Vector3[] cornerPoints = ColliderUtils.GetCapsuleColliderCornerPoints(_capsuleCollider);
        foreach (Vector3 cornerPoint in cornerPoints)
        {
            Gizmos.DrawSphere(cornerPoint, 0.05f);
        }
    }

    protected void Start()
    {
        base.Start();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _limbSpawner = GetComponent<LimbSpawner>();
    }

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
        _limbSpawner.SpawnLimb(transform.position, transform.rotation);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        healthComponent.ResetHealth();
    }

    public override bool IsColliderVisibleFrom(Vector3 point, LayerMask obstacleMask)
    {
        Vector3[] cornerPoints = ColliderUtils.GetCapsuleColliderCornerPoints(_capsuleCollider);
        foreach (Vector3 cornerPoint in cornerPoints)
        {
            Vector3 directionToCornerPoint = (cornerPoint - point).normalized;

            float distanceBias = 0.1f;
            float distance = Vector3.Distance(cornerPoint, point) + distanceBias;

            Vector3 shiftedPoint = point - directionToCornerPoint * 0.01f;
            if (!Physics.Raycast(shiftedPoint, directionToCornerPoint, distance, obstacleMask))
            {
                Debug.DrawRay(shiftedPoint, directionToCornerPoint * (distance - distanceBias), Color.red, 3f);
                return true;
            }

        }
        return false;
    }
}
