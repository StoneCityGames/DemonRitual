using DG.Tweening;
using UnityEngine;

public class MeleeView : MonoBehaviour
{
    [SerializeField] private MeleeController _meleeController;
    [SerializeField] private Transform _meleeUI;

    private Tweener _tween = null;

    private void OnEnable()
    {
        _meleeController.OnMeleeActivate += HandleMeleeActivate;
    }

    private void OnDisable()
    {
        _meleeController.OnMeleeActivate -= HandleMeleeActivate;
    }

    private void HandleMeleeActivate(bool isActive)
    {
        if (isActive)
        {
            StartAnimation();
        }
        else
        {
            StopAnimation();
        }
    }

    private void StartAnimation()
    {
        if (_tween != null && _tween.IsPlaying())
        {
            return;
        }

        _tween = _meleeUI.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f).SetLoops(-1, LoopType.Yoyo);
    }

    private void StopAnimation()
    {
        if (_tween == null || _tween.IsComplete())
        {
            return;
        }

        _tween.Kill();
        _meleeUI.DOScale(new Vector3(1f, 1f, 1f), 1f);
    }
}
