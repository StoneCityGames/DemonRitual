using UnityEngine;

public class LimbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _limbPrefab;
    [SerializeField, Range(0, 1)] private float _spawnChance = 0.5f;
    [SerializeField] private float _limbForcePower = 2f;

    public void SpawnLimb(Vector3 pos, Quaternion rotation)
    {
        if (Random.value <= _spawnChance)
        {
            GameObject gameObject = Instantiate(_limbPrefab, pos, rotation);
            if (gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _limbForcePower, ForceMode.Impulse);
            }
        }
    }
}
