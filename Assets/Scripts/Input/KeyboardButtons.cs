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
    }
}
