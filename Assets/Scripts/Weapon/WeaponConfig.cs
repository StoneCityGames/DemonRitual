using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Scriptable Objects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField, Tooltip("An amount of damage that enemies will take")] private float _damage;
    [SerializeField, Tooltip("Reload time in seconds")] private float _reloadTime;
    [SerializeField, Tooltip("Max shot distance")] private float _maxDistance;
    [SerializeField] private AudioClip _firingSound;

    public float Damage { get { return _damage; } }
    public float ReloadTime { get { return _reloadTime; } }
    public float MaxDistance { get { return _maxDistance; } }
    public AudioClip FiringSound { get { return _firingSound; } }
}
