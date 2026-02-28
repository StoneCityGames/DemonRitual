using UnityEngine;

public class CrossbowAlternateAmmoPickup : AutoPickup
{
    [SerializeField] private uint _ammo = 5;

    protected override bool HandleTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WeaponController weaponController))
        {
            Crossbow crossbow = weaponController.GetCrossbow();
            if (crossbow == null || crossbow.AlternateAmmo == crossbow.MaxAlternateAmmo)
            {
                return false;
            }
            crossbow.SetAlternateAmmo(crossbow.AlternateAmmo + _ammo);
            return true;
        }
        return false;
    }
}
