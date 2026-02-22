using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public float ReloadTime => _currentWeapon.ReloadTime;
    public float AlternateReloadTime => _currentWeapon.AlternateReloadTime;
    public float LastShootTime => _currentWeapon.LastShootTime;
    public float LastAlternateShootTime => _currentWeapon.LastAlternateShootTime;

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
