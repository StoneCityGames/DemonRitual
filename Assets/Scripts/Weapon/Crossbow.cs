using System;
using UnityEngine;

public class Crossbow : Weapon
{
    [Serializable]
    private class CrossbowModeConfig
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

    [Serializable]
    private class CrossbowAlternateModeConfig : CrossbowModeConfig
    {
        [SerializeField, Tooltip("An area radius of explosion")] private float _explosionRadius = 3f;

        public float ExplosionRadius { get { return _explosionRadius; } }
    }

    private struct HitInfo
    {
        public Vector3 Point;
        public bool IsHit;
        public Collider Collider;
    }

    [SerializeField] private CrossbowModeConfig _defaultMode;
    [SerializeField] private CrossbowAlternateModeConfig _alternateMode;
    [SerializeField, Tooltip("An origin transform of a projectile")] private Transform _origin;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _tracePrefab;
    [SerializeField] private Transform _traceOrigin;

    private float _lastDefaultModeShootTime = 0f;
    private float _lastAlternateModeShootTime = 0f;
    private readonly Collider[] _colliders = new Collider[256];
    private readonly WeaponTrace[] _weaponTraces = new WeaponTrace[8];
    private int _currentWeaponTraceIndex = 0;

    private void Start()
    {
        AllocateWeaponTraces();
    }

    public override bool CanShoot()
    {
        return Time.time - _lastDefaultModeShootTime > _defaultMode.ReloadTime;
    }

    public override bool CanShootAlternate()
    {
        return Time.time - _lastAlternateModeShootTime > _alternateMode.ReloadTime;
    }

    public override void Shoot()
    {
        _audioSource.PlayOneShot(_defaultMode.FiringSound);

        HitInfo hitInfo = TraceShot(_defaultMode);
        if (hitInfo.IsHit)
        {
            Debug.Log($"Hit object {hitInfo.Collider.gameObject}");
            {
                if (hitInfo.Collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_defaultMode.Damage);
                }
            }

        }

        DrawTrace(_traceOrigin.position, hitInfo.Point);

        _lastDefaultModeShootTime = Time.time;
    }

    public override void ShootAlternate()
    {
        _audioSource.PlayOneShot(_alternateMode.FiringSound);

        HitInfo hitInfo = TraceShot(_alternateMode);
        if (hitInfo.IsHit)
        {
            int numHits = Physics.OverlapSphereNonAlloc(hitInfo.Point, _alternateMode.ExplosionRadius, _colliders, _alternateMode.LayerMask);
            for (int i = 0; i < numHits; i++)
            {
                Collider collider = _colliders[i];

                Debug.Log($"Hit object {collider.gameObject}");

                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_alternateMode.Damage);
                }
            }
        }

        DrawTrace(_traceOrigin.position, hitInfo.Point);

        _lastAlternateModeShootTime = Time.time;
    }

    private HitInfo TraceShot(CrossbowModeConfig mode)
    {
        bool isHit = Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hitInfo, mode.MaxDistance, mode.LayerMask);
        return new HitInfo { Collider = hitInfo.collider, IsHit = isHit, Point = isHit ? hitInfo.point : _origin.forward * mode.MaxDistance };
    }

    private void DrawTrace(Vector3 start, Vector3 end)
    {
        WeaponTrace trace = _weaponTraces[_currentWeaponTraceIndex];
        trace.Respawn(start, end);
        _currentWeaponTraceIndex = (_currentWeaponTraceIndex + 1) % _weaponTraces.Length;
    }

    private void AllocateWeaponTraces()
    {
        for (int i = 0; i < _weaponTraces.Length; i++)
        {
            GameObject prefab = Instantiate(_tracePrefab);
            WeaponTrace weaponTrace = prefab.GetComponent<WeaponTrace>();
            if (weaponTrace == null)
            {
                throw new Exception("Trace prefab has no WeaponTrace component");
            }
            _weaponTraces[i] = weaponTrace;
        }
    }
}
