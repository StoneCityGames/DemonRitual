using UnityEngine;

[RequireComponent(typeof(LookController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(MeleeController))]
[RequireComponent(typeof(PickupController))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(UIController))]
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

        _meleeController.SetCurrentHits(_meleeController.MaxHits);
    }

    private void Update()
    {
        if (_gameController.Pause)
        {
            return;
        }

        _lookController.Think(_input);
        _movementController.Think(_input);
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
        _input.Player.Sprint.started += _lookController.OnSprintStarted;
        _input.Player.Sprint.canceled += _lookController.OnSprintCancelled;

        _input.Player.Sprint.started += _movementController.OnSprintStarted;
        _input.Player.Sprint.canceled += _movementController.OnSprintCanceled;

        _input.Player.Attack.performed += OnAttackPermormed;
        _input.Player.AttackAlternate.performed += OnAlternateAttackPerformed;
        _input.Player.MeleeAttack.performed += OnMeleeAttackPerformed;

        _input.Player.Interact.performed += OnInteractPerformed;
    }

    private void UnbindPlayerControls()
    {
        _input.Player.Sprint.started -= _lookController.OnSprintStarted;
        _input.Player.Sprint.canceled -= _lookController.OnSprintCancelled;

        _input.Player.Sprint.started -= _movementController.OnSprintStarted;
        _input.Player.Sprint.canceled -= _movementController.OnSprintCanceled;

        _input.Player.Attack.performed -= OnAttackPermormed;
        _input.Player.AttackAlternate.performed -= OnAlternateAttackPerformed;
        _input.Player.MeleeAttack.performed -= OnMeleeAttackPerformed;

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
        if (_movementController.IsSprinting)
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
