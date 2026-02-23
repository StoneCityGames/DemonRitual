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

    private const string FOV_PREF_KEY = "FOV";
    private const string SENS_PREF_KEY = "SENSITIVITY";
    private const string VOLUME_PREF_KEY = "VOLUME";

    private void OnEnable()
    {
        _fovSettingInput.OnValueChanged += OnFovChanged;
        _sensitivitySettingInput.OnValueChanged += OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged += OnVolumeChanged;
        _quitButton.onClick.AddListener(OnQuit);

        _fovSettingInput.SetValue(_camera.fieldOfView);
        _sensitivitySettingInput.SetValue(_playerConfig.MouseSensitivity);
        _volumeSettingInput.SetValue(GetVolume());
    }

    private void OnDisable()
    {
        _fovSettingInput.OnValueChanged -= OnFovChanged;
        _sensitivitySettingInput.OnValueChanged -= OnSensitivityChanged;
        _volumeSettingInput.OnValueChanged -= OnVolumeChanged;
        _quitButton.onClick.RemoveListener(OnQuit);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(FOV_PREF_KEY, _camera.fieldOfView);
        PlayerPrefs.SetFloat(SENS_PREF_KEY, _playerConfig.MouseSensitivity);
        PlayerPrefs.SetFloat(VOLUME_PREF_KEY, GetVolume());
    }

    public void LoadPrefs()
    {
        float savedFov = PlayerPrefs.GetFloat(FOV_PREF_KEY, float.MaxValue);
        float savedSens = PlayerPrefs.GetFloat(SENS_PREF_KEY, float.MaxValue);
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREF_KEY, float.MaxValue);

        _camera.fieldOfView = savedFov;
        _playerConfig.MouseSensitivity = savedSens;
        SetVolume(savedVolume);
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
