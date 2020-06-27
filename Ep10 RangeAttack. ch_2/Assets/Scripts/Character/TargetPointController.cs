using UnityEngine;

public class TargetPointController : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetButton("Fire2"))
        {
            var newPosition = new Vector3(transform.position.x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
