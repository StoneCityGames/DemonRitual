using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Limb : MonoBehaviour, IPickup
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _pickupColor;

    private Outline _outline;

    public void Start()
    {
        _outline = GetComponent<Outline>();
        Outline(false);
    }

    public void PickUp(Player player)
    {
        player.TakeNewMelee();
        Destroy(this);
        Destroy(gameObject, 0.1f);
    }

    public void Outline(bool enabled)
    {
        if (enabled)
        {
            SetPickupColor();
        }
        else
        {
            SetDefaultColor();
        }
    }

    private void SetDefaultColor()
    {
        _outline.OutlineColor = _defaultColor;
    }

    private void SetPickupColor()
    {
        _outline.OutlineColor = _pickupColor;
    }
}
