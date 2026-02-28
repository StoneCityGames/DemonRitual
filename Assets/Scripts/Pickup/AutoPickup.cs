using UnityEngine;

public class AutoPickup : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _respawnTime = 2f;
    [SerializeField] private AudioClip _pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if ((_layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            bool isUsed = HandleTriggerEnter(other);
            if (isUsed)
            {
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position);
                gameObject.SetActive(false);
                Invoke(nameof(Respawn), _respawnTime);
            }
        }
    }

    protected virtual bool HandleTriggerEnter(Collider other)
    {
        return true;
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
