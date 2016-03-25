using UnityEngine;
using System.Collections;

public class VirtualButtons : Buttons
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (viewportPoint.x < 0.5f)
            {
                OnLeftButtonPressed();
            }
            else
            {
                OnRightButtonPressed();
            }
        }
    }
}
