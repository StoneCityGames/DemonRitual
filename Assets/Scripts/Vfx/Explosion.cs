using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;

    private ParticleSystem[] _particleSystems = new ParticleSystem[4];
    private int _currentParticleIndex = 0;

    private void Start()
    {
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            GameObject gameObject = Instantiate(_particlePrefab);
            ParticleSystem particle = gameObject.GetComponent<ParticleSystem>();
            if (particle == null)
            {
                throw new System.Exception("Prefab has no ParticleSystem component");
            }
            _particleSystems[i] = particle;
        }
    }

    public void PlayAt(Vector3 position)
    {
        ParticleSystem particle = _particleSystems[_currentParticleIndex];
        particle.transform.position = position;

        if (particle.isPlaying)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        particle.Play();

        _currentParticleIndex = (_currentParticleIndex + 1) % _particleSystems.Length;
    }
}
