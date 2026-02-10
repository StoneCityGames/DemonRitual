using System;
using UnityEngine;

public class Crossbow : Weapon
{
    [Serializable]
    private struct CrossbowModeConfig
    {
        [SerializeField, Tooltip("An amount of damage that enemies will take")] private float _damage;
        [SerializeField, Tooltip("Reload time in seconds")] private float _reloadTime;
        [SerializeField, Tooltip("Max shot distance")] private float _maxDistance;
        [SerializeField, Tooltip("The sound that will be played when a shot is fired")] private AudioClip _firingSound;
        [SerializeField, Tooltip("Layer mask for raycasts")] private LayerMask _layerMask;

        public float Damage { get { return _damage; } }
        public float ReloadTime { get { return _reloadTime; } }
        public float MaxDistance { get { return _maxDistance; } }
        public AudioClip FiringSound { get { return _firingSound; } }
        public LayerMask LayerMask { get { return _layerMask; } }
    }

    [SerializeField] private CrossbowModeConfig _defaultMode;
    [SerializeField] private CrossbowModeConfig _alternateMode;
    [SerializeField, Tooltip("An origin transform of a projectile")] private Transform _origin;
    [SerializeField] private AudioSource _audioSource;

    private float _lastShootTime = 0f;

    public override bool CanShoot()
    {
        return Time.time - _lastShootTime > _defaultMode.ReloadTime;
    }

    public override void CanShootAlternate()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        _audioSource.PlayOneShot(_defaultMode.FiringSound);

        bool isHit = Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hitInfo, _defaultMode.MaxDistance, _defaultMode.LayerMask);
        if (isHit)
        {
            Debug.Log($"Hit object {hitInfo.collider.gameObject}");
            {

                if (hitInfo.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_defaultMode.Damage);
                    if (enemy.IsDead())
                    {
                        Debug.Log($"Killed an enemy ${hitInfo.collider.gameObject.ToString()}");
                    }
                }
            }

            _lastShootTime = Time.time;
        }
    }

    public override void ShootAlternate()
    {
        throw new System.NotImplementedException();
    }
}
