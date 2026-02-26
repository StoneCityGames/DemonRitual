using TMPro;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _alternateAmmoText;
    [SerializeField] private WeaponController _weaponController;

    private void Start()
    {
        _ammoText.text = _weaponController.Ammo.ToString();
        _alternateAmmoText.text = _weaponController.AlternateAmmo.ToString();
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
        _ammoText.text = ammo.ToString();
    }

    private void OnAlternateAmmoChanged(uint ammo)
    {
        _alternateAmmoText.text = ammo.ToString();
    }
}
