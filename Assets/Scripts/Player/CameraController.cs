using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomFactor = 2f;

    private bool _isZoomed = false;

    public bool IsZoomed { get { return _isZoomed; } }

    public void Zoom()
    {
        if (_isZoomed)
        {
            _camera.fieldOfView *= _zoomFactor;
        }
        else
        {
            _camera.fieldOfView /= _zoomFactor;
        }
        _isZoomed = !_isZoomed;
    }

    public float GetFOV()
    {
        return _camera.fieldOfView * GetZoomFactor();
    }

    public void SetFOV(float fov)
    {
        _camera.fieldOfView = fov / GetZoomFactor();
    }

    private float GetZoomFactor()
    {
        return _isZoomed ? _zoomFactor : 1f;
    }
}
