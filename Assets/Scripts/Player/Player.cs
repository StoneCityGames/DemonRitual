using UnityEngine;

[RequireComponent(typeof(LookController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(WeaponController))]
public class Player : MonoBehaviour
{
    private MovementController _movementController;
    private LookController _lookController;
    private WeaponController _weaponController;
    private DefaultInputSystem _input;

    private void Awake()
    {
        _input = new DefaultInputSystem();

        _movementController = GetComponent<MovementController>();
        _lookController = GetComponent<LookController>();
        _weaponController = GetComponent<WeaponController>();
    }

    private void Update()
    {
        _lookController.Think(_input);
        _movementController.Think(_input);
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Sprint.started += _lookController.OnSprintStarted;
        _input.Player.Sprint.canceled += _lookController.OnSprintCancelled;

        _input.Player.Sprint.started += _movementController.OnSprintStarted;
        _input.Player.Sprint.canceled += _movementController.OnSprintCanceled;

        _input.Player.Attack.performed += OnAttackPermormed;
    }

    private void OnAttackPermormed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting)
        {
            return;
        }

        _weaponController.Shoot();
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Sprint.started -= _lookController.OnSprintStarted;
        _input.Player.Sprint.canceled -= _lookController.OnSprintCancelled;

        _input.Player.Sprint.started -= _movementController.OnSprintStarted;
        _input.Player.Sprint.canceled -= _movementController.OnSprintCanceled;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _movementController.HandleColliderHit(hit);
    }
}
