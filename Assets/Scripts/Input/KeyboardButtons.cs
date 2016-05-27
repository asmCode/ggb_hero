using UnityEngine;
using System.Collections;

public class KeyboardButtons : Buttons
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            OnLeftButtonPressed();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            OnRightButtonPressed();

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            OnLeftButtonReleased();
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            OnRightButtonReleased();
    }
}
