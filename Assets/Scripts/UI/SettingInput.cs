using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingInput : MonoBehaviour
{
    public event Action<float> OnValueChanged;

    public float MaxValue { get { return _slider.maxValue; } }
    public float MinValue { get { return _slider.minValue; } }

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _valueField;

    private void Awake()
    {
        _valueField.text = _slider.value.ToString();
    }

    public void SetValue(float value)
    {
        _slider.value = Mathf.Clamp(value, MinValue, MaxValue);
        HandleValueChange();
    }

    public void HandleValueChange()
    {
        _valueField.text = _slider.value.ToString();
        OnValueChanged?.Invoke(_slider.value);
    }
}
