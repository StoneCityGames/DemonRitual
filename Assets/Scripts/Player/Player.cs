using UnityEngine;

[RequireComponent(typeof(LookController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(MeleeController))]
[RequireComponent(typeof(PickupController))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(UIController))]
[RequireComponent(typeof(CameraController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    private MovementController _movementController;
    private LookController _lookController;
    private WeaponController _weaponController;
    private MeleeController _meleeController;
    private PickupController _pickupController;
    private HealthComponent _healthComponent;
    private UIController _uiController;
    private CameraController _cameraController;
    private DefaultInputSystem _input;

    private void Awake()
    {
        _input = new DefaultInputSystem();

        _movementController = GetComponent<MovementController>();
        _lookController = GetComponent<LookController>();
        _weaponController = GetComponent<WeaponController>();
        _meleeController = GetComponent<MeleeController>();
        _pickupController = GetComponent<PickupController>();
        _healthComponent = GetComponent<HealthComponent>();
        _uiController = GetComponent<UIController>();
        _cameraController = GetComponent<CameraController>();

        _meleeController.SetCurrentHits(_meleeController.MaxHits);
    }

    private void Update()
    {
        if (_gameController.Pause)
        {
            return;
        }

        _lookController.Think(_input);
        _movementController.Think(_input, _cameraController.IsZoomed);
    }

    private void OnEnable()
    {
        _input.Enable();

        BindPlayerControls();
        BindUIControls();
    }

    private void OnDisable()
    {
        _input.Disable();

        UnbindPlayerControls();
        UnbindUIControls();
    }

    public void TakeNewMelee()
    {
        _meleeController.SetCurrentHits(0);
    }

    private void BindPlayerControls()
    {
        _input.Player.Sprint.started += OnSprintStarted;
        _input.Player.Sprint.canceled += OnSprintCanceled;

        _input.Player.Attack.performed += OnAttackPermormed;
        _input.Player.AttackAlternate.performed += OnAlternateAttackPerformed;
        _input.Player.MeleeAttack.performed += OnMeleeAttackPerformed;
        _input.Player.Zoom.performed += OnZoomPerformed;

        _input.Player.Interact.performed += OnInteractPerformed;
    }

    private void UnbindPlayerControls()
    {
        _input.Player.Sprint.started -= OnSprintStarted;
        _input.Player.Sprint.canceled -= OnSprintCanceled;

        _input.Player.Attack.performed -= OnAttackPermormed;
        _input.Player.AttackAlternate.performed -= OnAlternateAttackPerformed;
        _input.Player.MeleeAttack.performed -= OnMeleeAttackPerformed;
        _input.Player.Zoom.performed -= OnZoomPerformed;

        _input.Player.Interact.performed -= OnInteractPerformed;
    }

    private void BindUIControls()
    {
        _input.Player.OpenMenu.performed += OnOpenMenu;
    }

    private void UnbindUIControls()
    {
        _input.Player.OpenMenu.performed -= OnOpenMenu;
    }

    private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting)
        {
            return;
        }

        _pickupController.PickUpCurrentItem();
    }

    private void OnMeleeAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting || _cameraController.IsZoomed)
        {
            return;
        }

        _meleeController.Attack();
    }

    private void OnAlternateAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting)
        {
            return;
        }

        _weaponController.ShootAlternate();
    }

    private void OnAttackPermormed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting)
        {
            return;
        }

        _weaponController.Shoot();
    }

    private void OnZoomPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_movementController.IsSprinting)
        {
            return;
        }

        _cameraController.Zoom();
    }

    private void OnSprintStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_cameraController.IsZoomed)
        {
            return;
        }

        _lookController.StartSprint();
        _movementController.StartSprint();
    }

    private void OnSprintCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _lookController.CancelSprint();
        _movementController.CancelSprint();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _movementController.HandleColliderHit(hit);
    }

    private void OnOpenMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _gameController.Pause = !_gameController.Pause;
        if (_gameController.Pause)
        {
            _uiController.ShowMenu();
            UnbindPlayerControls();
        }
        else
        {
            _uiController.ShowHUD();
            BindPlayerControls();
        }
    }
}
