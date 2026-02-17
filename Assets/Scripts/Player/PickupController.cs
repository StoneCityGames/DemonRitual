using UnityEngine;

[RequireComponent(typeof(Player))]
public class PickupController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layer;

    IPickup _currentPickup = null;
    Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        bool isHit = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, _distance, _layer);
        if (isHit && hitInfo.collider.gameObject.TryGetComponent(out IPickup pickup))
        {
            _currentPickup = pickup;
            _currentPickup.Outline(true);
        }
        else
        {
            _currentPickup?.Outline(false);
            _currentPickup = null;
        }
    }

    public void PickUpCurrentItem()
    {
        if (_currentPickup == null)
        {
            return;
        }

        _currentPickup.PickUp(_player);
        _currentPickup = null;
    }
}
