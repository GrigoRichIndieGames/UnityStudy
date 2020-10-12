using UnityEngine;
using UnityEngine.EventSystems;

public class Notch : MonoBehaviour, IDropHandler
{
    public string ID;


    public void OnDrop(PointerEventData touch)
    {
        if ((object)touch.pointerDrag != null)
        {            
            var figure = touch.pointerDrag.GetComponent<Figure>();

            if (figure.ID.Equals(ID))
            {
                
                figure.transform.position = transform.position;
            }
        }
    }
}
