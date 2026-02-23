using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    private enum MovementState
    {
        Ground,
        Air
    }

    [SerializeField] private Transform _body;

    [Header("Movement")]
    [SerializeField, Tooltip("Base maximum movement speed when walking on the ground (units per second)")] private float _groundSpeed = 12f;
    [SerializeField, Tooltip("Rate at which the player accelerates to ground speed")] private float _groundAcceleration = 10f;
    [SerializeField, Tooltip("Friction/resistance applied when moving on the ground. Higher values mean quicker stops")] private float _groundFriction = 8f;
    [SerializeField, Tooltip("Speed threshold. If current speed is below this, the player comes to a complete stop")] private float _groundStopSpeed = 4f;

    [Space]

    [SerializeField, Tooltip("Maximum movement speed when sprinting on the ground (units per second)")] private float _groundSprintSpeed = 18f;
    [SerializeField, Tooltip("Rate at which the player accelerates to sprint speed")] private float _groundSprintAcceleration = 14f;
    [SerializeField, Tooltip("Friction applied specifically when sprinting. Can differ from walking friction")] private float _groundSprintFriction = 4f;
    [SerializeField, Tooltip("Sideways movement speed when sprinting")] private float _groundSprintSidewaysSpeed = 4f;

    [Space]

    [SerializeField, Tooltip("Maximum horizontal movement speed while in the air (units per second)")] private float _airSpeed = 5f;
    [SerializeField, Tooltip("Rate of horizontal acceleration while airborne")] private float _airAcceleration = 8f;
    [SerializeField, Tooltip("Air resistance/friction. Much lower than ground for a floaty, momentum-based feel")] private float _airFriction = 0.1f;

    [Space]

    [SerializeField, Tooltip("Time (in seconds) after walking off a ledge during which the player can still jump")] private float _coyoteTime = 0.2f;
    [SerializeField, Tooltip("Strength of the downward gravitational force applied")] private float _gravity = 28.6f;
    [SerializeField, Tooltip("The height (in world units) the player's jump will reach")] private float _jumpHeight = 1.62f;
    [SerializeField, Tooltip("Downward force applied when the player's head hits a ceiling")] private float _ceilingBumpForce = 1f;

    public bool IsSprinting { get { return _wantsToSprint; } }

    private CharacterController _characterController;

    private float _lastGroundTime = 0f;
    private MovementState _movementState = MovementState.Ground;
    private Vector3 _velocity = Vector3.zero;
    private bool _wantsToJump = false;
    private bool _wantsToSprint = false;
    private Vector2 _moveInput = Vector2.zero;

    public void Think(DefaultInputSystem input, bool isZoomed)
    {
        _moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector3 wishDir = _body.forward * _moveInput.y + _body.right * _moveInput.x;
        wishDir.Normalize();

        UpdateMovementState();

        if (input.Player.Jump.WasPerformedThisFrame() && !isZoomed)
        {
            _wantsToJump = true;
        }

        switch (_movementState)
        {
            case MovementState.Ground:
                HandleGroundMovement(wishDir);
                break;
            case MovementState.Air:
                HandleAirMovement(wishDir);
                break;
        }

        _characterController.Move(_velocity * Time.deltaTime);
    }

    public void HandleColliderHit(ControllerColliderHit hit)
    {
        HandleCeilingCollision(hit);
    }

    public void StartSprint()
    {
        _wantsToSprint = true;
    }

    public void CancelSprint()
    {
        _wantsToSprint = false;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void UpdateMovementState()
    {
        bool isGrounded = _characterController.isGrounded;

        if (isGrounded)
        {
            _lastGroundTime = Time.time;
            _movementState = MovementState.Ground;
        }
        else if (Time.time - _lastGroundTime > _coyoteTime)
        {
            _movementState = MovementState.Air;
        }
    }

    private void HandleGroundMovement(Vector3 wishDir)
    {
        float groundSpeed = _groundSpeed;
        float groundAcceleration = _groundAcceleration;
        float groundFriction = _groundFriction;

        if (_wantsToSprint)
        {
            if (IsMovingForward())
            {
                groundSpeed = _groundSprintSpeed;
            }
            else
            {
                groundSpeed = _groundSprintSidewaysSpeed;
            }

            groundFriction = _groundSprintFriction;
            groundAcceleration = _groundSprintAcceleration;
        }

        ApplyFriction(groundFriction);
        Accelerate(wishDir, groundSpeed, groundAcceleration);

        if (_wantsToJump)
        {
            Jump();
            _movementState = MovementState.Air;
            _wantsToJump = false;
        }
        else
        {
            // Reset the gravity velocity
            _velocity.y = -_gravity * Time.deltaTime;
        }
    }

    private void HandleAirMovement(Vector3 wishDir)
    {
        ApplyFriction(_airFriction);
        Accelerate(wishDir, _airSpeed, _airAcceleration);

        // Apply gravity
        _velocity.y -= _gravity * Time.deltaTime;
    }

    private void ApplyFriction(float friction)
    {
        float horizontalSpeed = new Vector3(_velocity.x, 0f, _velocity.z).magnitude;
        if (horizontalSpeed < 0.01f)
        {
            _velocity = Vector3.Scale(_velocity, Vector3.up);
            return;
        }

        float control = horizontalSpeed;
        if (_movementState == MovementState.Ground)
        {
            control = horizontalSpeed < _groundStopSpeed ? _groundStopSpeed : horizontalSpeed;
        }
        float drop = control * friction * Time.deltaTime;

        float newSpeed = Mathf.Max(horizontalSpeed - drop, 0.0f) / horizontalSpeed;
        _velocity.x *= newSpeed;
        _velocity.z *= newSpeed;
    }

    private void Accelerate(Vector3 wishDir, float wishSpeed, float accel)
    {
        float alignment = Vector3.Dot(_velocity, wishDir);
        float addSpeed = wishSpeed - alignment;
        if (addSpeed <= 0f)
            return;

        float accelSpeed = Mathf.Min(accel * wishSpeed * Time.deltaTime, addSpeed);
        _velocity += accelSpeed * wishDir;
    }

    private void Jump()
    {
        _velocity.y = Mathf.Sqrt(2f * _jumpHeight * _gravity);
    }

    private void HandleCeilingCollision(ControllerColliderHit hit)
    {
        float angle = Vector3.Angle(hit.normal, Vector3.down);
        if (angle < 45f && _velocity.y > 0)
        {
            _velocity.y = -_ceilingBumpForce;
        }
    }

    private bool IsMovingForward()
    {
        return _moveInput.y > 0 && _moveInput.x == 0;
    }
}
