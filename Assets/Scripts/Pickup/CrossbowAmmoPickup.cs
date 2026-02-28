using UnityEngine;

public class CrossbowAmmoPickup : AutoPickup
{
    [SerializeField] private uint _ammo = 5;

    protected override bool HandleTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WeaponController weaponController))
        {
            Crossbow crossbow = weaponController.GetCrossbow();
            if (crossbow == null || crossbow.Ammo == crossbow.MaxAmmo)
            {
                return false;
            }
            crossbow.SetAmmo(crossbow.Ammo + _ammo);
            return true;
        }
        return false;
    }
}
