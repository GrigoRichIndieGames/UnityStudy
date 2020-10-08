using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public float ZoomMax;
    public float ZoomMin;
    public float Sensitivity;

    private Camera _mainCamera;

    private Touch _touchA;
    private Touch _touchB;
    private Vector2 _touchADirection;
    private Vector2 _touchBDirection;
    private float _dstBtwTouchesPosition;
    private float _dstBtwTouchesDirections;
    private float _zoom;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            _touchA = Input.GetTouch(0);
            _touchB = Input.GetTouch(1);
            _touchADirection = _touchA.position - _touchA.deltaPosition;
            _touchBDirection = _touchB.position - _touchB.deltaPosition;

            _dstBtwTouchesPosition = Vector2.Distance(_touchA.position, _touchB.position);
            _dstBtwTouchesDirections = Vector2.Distance(_touchADirection, _touchBDirection);

            _zoom = _dstBtwTouchesPosition - _dstBtwTouchesDirections;

            var currentZoom = _mainCamera.orthographicSize - _zoom * Sensitivity;

            _mainCamera.orthographicSize = Mathf.Clamp(currentZoom, ZoomMin, ZoomMax);

            if (_touchBDirection != _touchB.position)
            {
                print("1");
                var angle = Vector3.SignedAngle(_touchB.position - _touchA.position,
                    _touchBDirection - _touchADirection, -_mainCamera.transform.forward);
                _mainCamera.transform.RotateAround(_mainCamera.transform.position, -_mainCamera.transform.forward, angle);
            }
        }
    }
}