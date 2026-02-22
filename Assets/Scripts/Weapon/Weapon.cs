using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot();

    public abstract bool CanShoot();

    public abstract void ShootAlternate();

    public abstract bool CanShootAlternate();

    public abstract float ReloadTime { get; }
    public abstract float AlternateReloadTime { get; }
    public abstract float LastShootTime { get; }
    public abstract float LastAlternateShootTime { get; }
}
