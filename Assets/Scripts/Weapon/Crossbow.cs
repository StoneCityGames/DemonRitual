using UnityEngine;

public class Crossbow : Weapon
{
    [SerializeField] private WeaponConfig _config;
    [SerializeField, Tooltip("An origin transform of a projectile")] private Transform _origin;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioSource _audioSource;

    private float _lastShootTime = 0f;

    public override bool CanShoot()
    {
        return Time.time - _lastShootTime > _config.ReloadTime;
    }

    public override void Shoot()
    {
        _audioSource.PlayOneShot(_config.FiringSound);

        bool isHit = Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hitInfo, _config.MaxDistance, _layerMask);
        if (isHit)
        {
            Debug.Log($"Hit object {hitInfo.collider.gameObject}");

            if (hitInfo.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_config.Damage);
                if (enemy.IsDead())
                {
                    Debug.Log($"Killed an enemy ${hitInfo.collider.gameObject.ToString()}");
                }
            }
        }

        _lastShootTime = Time.time;
    }
}
