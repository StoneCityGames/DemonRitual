using UnityEngine;

public class WeaponReloadView : MonoBehaviour
{
  [SerializeField] private WeaponController _weaponController;
  [SerializeField] private Transform _reloadFill;
  [SerializeField] private Transform _alternateReloadFill;

  private void Update()
  {
    SetFillScaleX(_reloadFill, CalculateReloadPercent(_weaponController.LastShootTime, _weaponController.ReloadTime, _weaponController.Ammo));
    SetFillScaleX(_alternateReloadFill, CalculateReloadPercent(_weaponController.LastAlternateShootTime, _weaponController.AlternateReloadTime, _weaponController.AlternateAmmo));
  }

  private float CalculateReloadPercent(float lastShootTime, float reloadTime, uint ammo)
  {
    return ammo > 0 ? Mathf.Clamp01((Time.time - lastShootTime) / reloadTime) : 0f;
  }

  private void SetFillScaleX(Transform fill, float scaleX)
  {
    Vector3 scale = fill.localScale;
    scale.x = scaleX;
    fill.localScale = scale;
  }
}
