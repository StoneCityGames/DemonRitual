using DG.Tweening;
using UnityEngine;

public class MeleeView : MonoBehaviour
{
    [SerializeField] private MeleeController _meleeController;
    [SerializeField] private Transform _animatedUI;
    [SerializeField] private UnityEngine.UI.Image _animatedImage;

    [SerializeField] private float _animationDuration = 2f;
    [SerializeField] private float _animationFallBackDuration = 1f;
    [SerializeField] private float _animationFadeEndValue = 0.2f;

    private Tweener _scaleTween = null;
    private Tweener _colorTween = null;
    private Color _initialAnimationColor;

    private void Awake()
    {
        _initialAnimationColor = _animatedImage.color;
    }

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
        if (_scaleTween != null && _scaleTween.IsPlaying() || _colorTween != null && _colorTween.IsPlaying())
        {
            return;
        }

        _scaleTween = _animatedUI.DOScale(new Vector3(1.5f, 1.5f, 1.5f), _animationDuration).SetLoops(-1, LoopType.Yoyo);
        _colorTween = _animatedImage.DOFade(_animationFadeEndValue, _animationDuration).SetLoops(-1, LoopType.Yoyo);
    }

    private void StopAnimation()
    {
        if (_scaleTween == null || _scaleTween.IsComplete())
        {
            return;
        }

        _scaleTween.Kill();
        _colorTween.Kill();

        _animatedUI.DOScale(new Vector3(1f, 1f, 1f), _animationFallBackDuration);
        _animatedImage.DOColor(_initialAnimationColor, _animationFallBackDuration);
    }
}
