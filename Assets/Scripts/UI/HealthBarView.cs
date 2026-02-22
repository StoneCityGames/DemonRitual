using UnityEngine;

public class HealthBarView : MonoBehaviour
{
  [SerializeField] private HealthComponent _healthComponent;
  [SerializeField] private Transform _fill;

  public void OnEnable()
  {
    _healthComponent.OnHealthChanged += HandleHealthChange;
  }

  public void OnDisable()
  {
    _healthComponent.OnHealthChanged -= HandleHealthChange;
  }

  private void HandleHealthChange(float newHealth)
  {
    Vector3 fillScale = _fill.localScale;
    fillScale.x = newHealth / _healthComponent.MaxHealth;
    _fill.localScale = fillScale;
  }
}
