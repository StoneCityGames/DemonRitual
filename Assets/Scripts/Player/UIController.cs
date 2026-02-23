using UnityEngine;

public class UIController : MonoBehaviour
{
    public bool IsMenuOpen { get { return _isMenuOpen; } }

    [SerializeField] private Canvas _hud;
    [SerializeField] private Canvas _menu;

    private bool _isMenuOpen = false;

    private void Start()
    {
        Cursor.visible = false;
    }

    public void ShowMenu()
    {
        _hud.gameObject.SetActive(false);
        _menu.gameObject.SetActive(true);

        Time.timeScale = 0f;

        AudioListener.pause = true;

        Cursor.visible = true;

        _isMenuOpen = true;
    }

    public void ShowHUD()
    {
        _hud.gameObject.SetActive(true);
        _menu.gameObject.SetActive(false);

        Time.timeScale = 1f;

        AudioListener.pause = false;

        Cursor.visible = false;

        _isMenuOpen = false;
    }
}
