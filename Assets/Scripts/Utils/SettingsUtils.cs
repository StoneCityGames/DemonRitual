using UnityEngine;

public static class SettingsUtils
{
    private const string FOV_PREF_KEY = "FOV";
    private const string SENS_PREF_KEY = "SENSITIVITY";
    private const string VOLUME_PREF_KEY = "VOLUME";

    public struct PlayerSettings
    {
        public float Fov;
        public float MouseSens;
        public float SoundVolume;
    }

    public static PlayerSettings ReadPlayerSettings()
    {
        float fov = PlayerPrefs.GetFloat(FOV_PREF_KEY, float.NaN);
        float mouseSens = PlayerPrefs.GetFloat(SENS_PREF_KEY, float.NaN);
        float soundVolume = PlayerPrefs.GetFloat(VOLUME_PREF_KEY, float.NaN);
        return new PlayerSettings
        {
            Fov = fov,
            MouseSens = mouseSens,
            SoundVolume = soundVolume
        };
    }

    public static void SavePlayerSettings(PlayerSettings settings)
    {
        if (float.IsFinite(settings.Fov))
        {
            PlayerPrefs.SetFloat(FOV_PREF_KEY, settings.Fov);
        }
        if (float.IsFinite(settings.MouseSens))
        {
            PlayerPrefs.SetFloat(SENS_PREF_KEY, settings.MouseSens);
        }
        if (float.IsFinite(settings.SoundVolume))
        {
            PlayerPrefs.SetFloat(VOLUME_PREF_KEY, settings.SoundVolume);
        }
    }
}
