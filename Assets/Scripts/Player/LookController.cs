using UnityEngine;

public class LookController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _body;
    [SerializeField] private PlayerConfig _playerSettings;

    [SerializeField] private float _sprintMouseSensitivityMultiplier = 0.2f;

    private Vector2 _rotationAngles = Vector2.zero;
    private float _mouseSensitivityMultiplier = 1f;

    public void Think(DefaultInputSystem input)
    {
        Vector2 lookInput = input.Player.Look.ReadValue<Vector2>();
        Vector2 lookInputScaled = Time.deltaTime * _playerSettings.MouseSensitivity * _mouseSensitivityMultiplier * lookInput;

        _rotationAngles.x += lookInputScaled.y;
        _rotationAngles.x = Mathf.Clamp(_rotationAngles.x, -90.0f, 90.0f);
        _rotationAngles.y += lookInputScaled.x;

        _camera.localRotation = Quaternion.Euler(_rotationAngles.x, 0.0f, 0.0f);
        _body.rotation = Quaternion.Euler(0.0f, _rotationAngles.y, 0.0f);
    }

    public void OnSprintStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _mouseSensitivityMultiplier = _sprintMouseSensitivityMultiplier;
    }

    public void OnSprintCancelled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _mouseSensitivityMultiplier = 1f;
    }
}
