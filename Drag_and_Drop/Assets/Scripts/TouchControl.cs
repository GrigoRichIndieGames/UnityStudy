using UnityEngine;
using UnityEngine.EventSystems;


public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Transform Square;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                Square.position += Vector3.right;
            }
            else
            {
                Square.position += Vector3.left;
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                Square.position += Vector3.up;
            }
            else
            {
                Square.position += Vector3.down;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
