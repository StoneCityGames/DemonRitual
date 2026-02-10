using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot();

    public abstract bool CanShoot();

    public abstract void ShootAlternate();

    public abstract void CanShootAlternate();
}
