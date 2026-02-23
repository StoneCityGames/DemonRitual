using System;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private int _maxEnemyHits = 1;
    [SerializeField] private float _coolDownSeconds = 0.5f;
    [SerializeField] private LayerMask _hitLayer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MeleeSounds _sounds;

    public int MaxHits { get { return _maxEnemyHits; } }
    public event Action<bool> OnMeleeActivate;

    private int _currentEnemyHits = 0;
    private float _lastAttackTime = 0f;

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

        bool isMeleeActive = _currentEnemyHits < _maxEnemyHits;
        OnMeleeActivate?.Invoke(isMeleeActive);
    }

    private void AttackRaycast()
    {
        if (Time.time - _lastAttackTime < _coolDownSeconds)
        {
            return;
        }

        bool isHit = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _distance, _hitLayer);
        if (isHit && hitInfo.collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log($"Hit enemy {enemy} with melee attack");
            enemy.TakeDamage(_damage, _camera.transform.forward, false);
            SetCurrentHits(_currentEnemyHits + 1);
            _audioSource.PlayOneShot(_sounds.HitSound, _sounds.VolumeScale);
        }
        else
        {
            _audioSource.PlayOneShot(_sounds.MissSound, _sounds.VolumeScale);
        }

        _lastAttackTime = Time.time;
    }

    [Serializable]
    private struct MeleeSounds
    {
        [SerializeField] private AudioClip _missSound;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private float _volumeScale;

        public AudioClip MissSound { get { return _missSound; } }
        public AudioClip HitSound { get { return _hitSound; } }
        public float VolumeScale { get { return _volumeScale; } }
    }
}
