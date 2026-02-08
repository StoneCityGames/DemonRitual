using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;

    public void Shoot()
    {
        if (!currentWeapon.CanShoot())
        {
            return;
        }

        currentWeapon.Shoot();
    }
}
