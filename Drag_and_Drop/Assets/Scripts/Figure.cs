using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Figure : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string ID;

    private Image _image;
    private Vector3 _offset;

    private void Start()
    {
        _image = GetComponent<Image>();
    }


    public void OnBeginDrag(PointerEventData touch)
    {
        _offset = transform.position - (Vector3)touch.position;
        _offset.z = 0.0f;

        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData touch)
    {
        var dragPosition = (Vector3)touch.position;
        dragPosition.z = 0.0f;
        transform.position = dragPosition + _offset;
    }

    public void OnEndDrag(PointerEventData touch)
    {
        _image.raycastTarget = true;
    }
}
