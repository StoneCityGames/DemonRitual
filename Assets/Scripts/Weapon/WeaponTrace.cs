using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WeaponTrace : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private float _startWidth = 0.15f;
    [SerializeField] private float _endWidth = 0.1f;

    private float _spawnTime = 0f;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        // Create a fading gradient
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.white, 0.0f),
                new GradientColorKey(Color.white, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),   // Fully opaque at start
                new GradientAlphaKey(0.0f, 1.0f)    // Fully transparent at end
            }
        );

        _lineRenderer.colorGradient = gradient;
        _lineRenderer.startWidth = _startWidth;
        _lineRenderer.endWidth = _endWidth;
        _lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        if (Time.time - _spawnTime > _fadeDuration)
        {
            gameObject.SetActive(false);
        }
    }

    public void Respawn(Vector3 start, Vector3 end)
    {
        gameObject.SetActive(true);

        _spawnTime = Time.time;
        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
    }
}
