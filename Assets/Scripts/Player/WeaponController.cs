using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Crossbow _crossbow;
    [SerializeField] private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public float ReloadTime => _currentWeapon.ReloadTime;
    public float AlternateReloadTime => _currentWeapon.AlternateReloadTime;
    public float LastShootTime => _currentWeapon.LastShootTime;
    public float LastAlternateShootTime => _currentWeapon.LastAlternateShootTime;
    public uint Ammo => _currentWeapon.Ammo;
    public uint AlternateAmmo => _currentWeapon.AlternateAmmo;

    public void Shoot()
    {
        if (!_currentWeapon.CanShoot())
        {
            return;
        }

        _currentWeapon.Shoot();
    }

    public void ShootAlternate()
    {
        if (!_currentWeapon.CanShootAlternate())
        {
            return;
        }

        _currentWeapon.ShootAlternate();
    }
}
