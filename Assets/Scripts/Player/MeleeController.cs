using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private LayerMask _hitLayer;

    bool _canAttack = false;

    public void Attack()
    {
        if (!_canAttack)
        {
            return;
        }

        AttackRaycast();
    }

    public void SetCanAttack(bool canAttack)
    {
        _canAttack = canAttack;
    }

    private void AttackRaycast()
    {
        bool isHit = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _distance, _hitLayer);
        if (isHit && hitInfo.collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log($"Hit enemy {enemy} with melee attack");
            enemy.TakeDamage(_damage);
        }
    }
}
