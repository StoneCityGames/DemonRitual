using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool _isPaused = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool Pause
    {
        get
        {
            return _isPaused;
        }
        set
        {
            _isPaused = value;
        }
    }
}
