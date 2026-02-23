using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _mouseSensitivity;

    public float MouseSensitivity
    {
        get { return _mouseSensitivity; }
        set { _mouseSensitivity = value; }
    }
}