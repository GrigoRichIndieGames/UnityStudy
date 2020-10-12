using UnityEngine;
using UnityEngine.UI;

public class TestJoystick : MonoBehaviour
{
    public Joystick Joystick;

    private void Update()
    {
        GetComponent<Text>().text = Joystick.Direction.ToString();
    }
}
