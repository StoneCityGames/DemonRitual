using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private int _maxEnemyHits = 1;
    [SerializeField] private LayerMask _hitLayer;

    public int MaxHits { get { return _maxEnemyHits; } }

    private int _currentEnemyHits = 0;

    public void Attack()
    {
        if (_currentEnemyHits >= _maxEnemyHits)
        {
            return;
        }

        AttackRaycast();
    }

    public void SetCurrentHits(int hits)
    {
        _currentEnemyHits = Mathf.Clamp(hits, 0, _maxEnemyHits);
    }

    private void AttackRaycast()
    {
        bool isHit = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _distance, _hitLayer);
        if (isHit && hitInfo.collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log($"Hit enemy {enemy} with melee attack");
            enemy.TakeDamage(_damage);
            SetCurrentHits(_currentEnemyHits + 1);
        }
    }
}
