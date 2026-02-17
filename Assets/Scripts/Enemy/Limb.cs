using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Limb : MonoBehaviour, IPickup
{
    Outline _outline;

    public void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void Outline(bool enabled)
    {
        _outline.enabled = enabled;
    }

    public void PickUp(Player player)
    {
        player.TakeNewMelee();
        Destroy(this);
        Destroy(gameObject, 0.1f);
    }
}
