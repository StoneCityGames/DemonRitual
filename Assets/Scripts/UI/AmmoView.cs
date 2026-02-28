using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _alternateAmmoText;
    [SerializeField] private WeaponController _weaponController;

    private void Start()
    {
        SetAmmoText(_weaponController.Ammo, _weaponController.MaxAmmo);
        SetAlternateAmmoText(_weaponController.AlternateAmmo, _weaponController.MaxAlternateAmmo);
    }

    private void OnEnable()
    {
        _weaponController.CurrentWeapon.OnAmmoChanged += OnAmmoChanged;
        _weaponController.CurrentWeapon.OnAlternateAmmoChanged += OnAlternateAmmoChanged;
    }

    private void OnDisable()
    {
        _weaponController.CurrentWeapon.OnAmmoChanged -= OnAmmoChanged;
        _weaponController.CurrentWeapon.OnAlternateAmmoChanged -= OnAlternateAmmoChanged;
    }

    private void OnAmmoChanged(uint ammo)
    {
        SetAmmoText(ammo, _weaponController.MaxAmmo);
    }

    private void OnAlternateAmmoChanged(uint ammo)
    {
        SetAlternateAmmoText(ammo, _weaponController.MaxAlternateAmmo);
    }

    private void SetAmmoText(uint ammo, uint maxAmmo)
    {
        _ammoText.text = $"{ammo}/{maxAmmo}";
    }

    private void SetAlternateAmmoText(uint ammo, uint maxAmmo)
    {
        _alternateAmmoText.text = $"{ammo}/{maxAmmo}";
    }
}
