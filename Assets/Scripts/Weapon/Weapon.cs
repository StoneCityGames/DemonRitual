using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public event Action<uint> OnAmmoChanged;
    public event Action<uint> OnAlternateAmmoChanged;

    protected void InvokeAmmoChangedEvent(uint ammo)
    {
        OnAmmoChanged?.Invoke(ammo);
    }

    protected void InvokeAlternateAmmoChangedEvent(uint ammo)
    {
        OnAlternateAmmoChanged?.Invoke(ammo);
    }

    public abstract void Shoot();
    public abstract bool CanShoot();
    public abstract void ShootAlternate();
    public abstract bool CanShootAlternate();

    public abstract float ReloadTime { get; }
    public abstract float AlternateReloadTime { get; }
    public abstract float LastShootTime { get; }
    public abstract float LastAlternateShootTime { get; }
    public abstract uint Ammo { get; }
    public abstract uint AlternateAmmo { get; }
}
