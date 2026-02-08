using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void TakeDamage(float damage);

    public abstract bool IsDead();
}
