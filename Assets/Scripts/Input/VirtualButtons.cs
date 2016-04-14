using UnityEngine;
using System.Collections;

public class VirtualButtons : Buttons
{
    void Update()
    {
        int lastTouchIndex = GetLastTouchIndex();
        if (lastTouchIndex == -1)
            return;

        Vector2 touchPosition = Input.GetTouch(lastTouchIndex).position;
        
        Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(touchPosition);
        if (viewportPoint.x < 0.5f)
        {
            OnLeftButtonPressed();
        }
        else
        {
            OnRightButtonPressed();
        }
    }

    private int GetLastTouchIndex()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
                return i;
        }

        return -1;
    }
}
