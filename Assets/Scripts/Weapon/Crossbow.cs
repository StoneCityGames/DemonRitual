using System;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Crossbow : Weapon
{
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
    private Explosion _explosion;
    private int _currentWeaponTraceIndex = 0;

    private void Start()
    {
        _explosion = GetComponent<Explosion>();

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
            if (hitInfo.Collider.TryGetComponent(out Enemy enemy))
            {
                Debug.Log($"Hit enemy {enemy}");
                enemy.TakeDamage(_defaultMode.Damage, _origin.forward);
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
                if (collider.TryGetComponent(out Enemy enemy) && enemy.IsColliderVisibleFrom(hitInfo.Point, _alternateMode.ObstacleLayerMask))
                {
                    Debug.Log($"Hit enemy {enemy}");
                    Vector3 direction = enemy.transform.position - hitInfo.Point;
                    enemy.TakeDamage(_alternateMode.Damage, direction);
                }
            }

            _explosion.PlayAt(hitInfo.Point);
        }

        DrawTrace(_traceOrigin.position, hitInfo.Point);

        _lastAlternateModeShootTime = Time.time;
    }

    private HitInfo TraceShot(CrossbowModeConfig mode)
    {
        bool isHit = Physics.Raycast(_origin.position, _origin.forward, out RaycastHit hitInfo, mode.MaxDistance, mode.LayerMask);
        return new HitInfo { Collider = hitInfo.collider, IsHit = isHit, Point = isHit ? hitInfo.point : _origin.position + _origin.forward * mode.MaxDistance };
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
        [SerializeField, Tooltip("Layer mask of the obstacles")] private LayerMask _obstacleLayerMask;

        public float ExplosionRadius { get { return _explosionRadius; } }
        public LayerMask ObstacleLayerMask { get { return _obstacleLayerMask; } }
    }

    private struct HitInfo
    {
        public Vector3 Point;
        public bool IsHit;
        public Collider Collider;
    }
}
