using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private bool _isPaused = false;

    private void Awake()
    {
        _settings.LoadPrefs();
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
