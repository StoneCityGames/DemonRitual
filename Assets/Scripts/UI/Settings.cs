using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PlayerConfig _playerConfig;

    [SerializeField] private SettingInput _fovSettingInput;
    [SerializeField] private SettingInput _sensitivitySettingInput;
    [SerializeField] private SettingInput _volumeSettingInput;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        _fovSettingInput.SetValue(_camera.fieldOfView);
        _sensitivitySettingInput.SetValue(_playerConfig.MouseSensitivity);
        _volumeSettingInput.SetValue(GetVolume());

        _fovSettingInput.OnValueChanged += OnFovChanged;
        _sensitivitySettingInput.OnValueChanged += OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged += OnVolumeChanged;
        _quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        _fovSettingInput.OnValueChanged -= OnFovChanged;
        _sensitivitySettingInput.OnValueChanged -= OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged -= OnVolumeChanged;
        _quitButton.onClick.RemoveListener(OnQuit);
    }

    private void OnFovChanged(float value)
    {
        _camera.fieldOfView = value;
    }

    private void OnSensitivityChanged(float value)
    {
        _playerConfig.MouseSensitivity = value;
    }

    private void OnVolumeChanged(float value)
    {
        SetVolume(value);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private float GetVolume()
    {
        return AudioListener.volume * (_volumeSettingInput.MaxValue - _volumeSettingInput.MinValue) + _volumeSettingInput.MinValue;
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = (value - _volumeSettingInput.MinValue) / (_volumeSettingInput.MaxValue - _volumeSettingInput.MinValue);
    }
}
