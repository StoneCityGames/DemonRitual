using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float _mouseSensitivity;

    public float MouseSensitivity
    {
        get { return _mouseSensitivity; }
    }
}