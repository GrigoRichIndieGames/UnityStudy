using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _stick;

    [SerializeField] private AxisOption _type;
    [SerializeField] private float _stickRange;
    [SerializeField] private float _deadZone;
    [SerializeField] private bool _isXRaw;
    [SerializeField] private bool _isYRaw;

    private Camera _camera;
    private Canvas _canvas;

    private Vector2 _input;

    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }
    public float Horizontal { get { return _isXRaw ? GetRawInput(_input.x, AxisOption.Horizontal) : _input.x; } }
    public float Vertical { get { return _isYRaw ? GetRawInput(_input.y, AxisOption.Horizontal) : _input.y; } }
    public float StickRange { get { return _stickRange; } set { _stickRange = Mathf.Abs(value); } }
    public float DeadZone { get { return _deadZone; } set { _deadZone = Mathf.Abs(value); } }
    public bool IsXRaw { get { return _isXRaw; } set { _isXRaw = value; } }
    public bool IsYRaw { get { return _isYRaw; } set { _isYRaw = value; } }

    private void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        var center = new Vector2(0.5f, 0.5f);
        _background.pivot = center;
        _stick.pivot = center;
        _stick.anchorMin = center;
        _stick.anchorMax = center;
        _stick.anchoredPosition = Vector2.zero;
    }

    private void FormatInput()
    {
        switch (_type)
        {
            case AxisOption.Horizontal:
                _input = new Vector2(_input.x, 0.0f);
                break;
            case AxisOption.Vertical:
                _input = new Vector2(0.0f, _input.y);
                break;
            default:
                break;
        }
    }

    private float GetRawInput(float value, AxisOption displaceAxis)
    {
        if (value == 0)
            return value;

        var returnValue = value;

        if (_type == AxisOption.Both)
        {
            var angle = Vector2.Angle(Vector2.up, _input);

            if (displaceAxis == AxisOption.Horizontal)
            {
                if (angle < 25 || angle > 155)
                    returnValue = 0;
                else
                    returnValue = value > 0 ? 1 : -1;
            }
            if (displaceAxis == AxisOption.Vertical)
            {
                if (angle > 65 || angle < 115)
                    returnValue = 0;
                else
                    returnValue = value > 0 ? 1 : -1;
            }
        }
        else
            returnValue = value > 0 ? 1 : -1;

        return returnValue;
    }

    public void OnPointerDown(PointerEventData touch)
    {
        OnDrag(touch);
    }

    public void OnDrag(PointerEventData touch)
    {
        _camera = _canvas.renderMode == RenderMode.ScreenSpaceCamera ? _canvas.worldCamera : null;

        var position = RectTransformUtility.WorldToScreenPoint(_camera, _background.position);
        var radius = _background.sizeDelta / 2;
        _input = (touch.position - position) / radius;

        var magnitude = _input.magnitude;
        if (magnitude > _deadZone)
        {
            if (magnitude > 0) _input = _input.normalized;
        }
        else
        {
            _input = Vector2.zero;
        }

        _stick.anchoredPosition = _input * radius * _stickRange;
    }

    public void OnPointerUp(PointerEventData touch)
    {
        _input = Vector2.zero;
        _stick.anchoredPosition = Vector2.zero;
    }

    private enum AxisOption
    {
        None = 0,
        Both = 1,
        Horizontal = 2,
        Vertical = 3
    }
}
