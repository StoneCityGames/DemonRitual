using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private CameraController _camera;
    [SerializeField] private PlayerConfig _playerConfig;

    [SerializeField] private SettingInput _fovSettingInput;
    [SerializeField] private SettingInput _sensitivitySettingInput;
    [SerializeField] private SettingInput _volumeSettingInput;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        _fovSettingInput.OnValueChanged += OnFovChanged;
        _sensitivitySettingInput.OnValueChanged += OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged += OnVolumeChanged;
        _quitButton.onClick.AddListener(OnQuit);

        _fovSettingInput.SetValue(_camera.GetFOV());
        _sensitivitySettingInput.SetValue(_playerConfig.MouseSensitivity);
        _volumeSettingInput.SetValue(GetVolume());
    }

    private void OnDisable()
    {
        _fovSettingInput.OnValueChanged -= OnFovChanged;
        _sensitivitySettingInput.OnValueChanged -= OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged -= OnVolumeChanged;
        _quitButton.onClick.RemoveListener(OnQuit);
        SavePrefs();
    }

    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    public void LoadPrefs()
    {
        SettingsUtils.PlayerSettings settings = SettingsUtils.ReadPlayerSettings();
        if (float.IsFinite(settings.Fov))
        {
            _camera.SetFOV(settings.Fov);
        }
        if (float.IsFinite(settings.MouseSens))
        {
            _playerConfig.MouseSensitivity = settings.MouseSens;
        }
        if (float.IsFinite(settings.SoundVolume))
        {
            SetVolume(settings.SoundVolume);
        }
    }

    private void SavePrefs()
    {
        SettingsUtils.SavePlayerSettings(new SettingsUtils.PlayerSettings
        {
            Fov = _camera.GetFOV(),
            MouseSens = _playerConfig.MouseSensitivity,
            SoundVolume = GetVolume()
        });
    }

    private void OnFovChanged(float value)
    {
        _camera.SetFOV(value);
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
