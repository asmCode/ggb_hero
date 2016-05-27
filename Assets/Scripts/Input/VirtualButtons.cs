using UnityEngine;
using System.Collections;

public class VirtualButtons : Buttons
{
    private int m_currentlyPressedId = -1;

    void Update()
    {
        int fingerId = 0;

        int lastTouchBeganIndex = GetLastTouchIndex(TouchPhase.Began, out fingerId);
        if (lastTouchBeganIndex != -1)
        {
            m_currentlyPressedId = fingerId;

            Vector2 touchPosition = Input.GetTouch(lastTouchBeganIndex).position;

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

        
        int lastTouchEndedIndex = GetLastTouchIndex(TouchPhase.Ended, out fingerId);
        if (lastTouchEndedIndex == -1)
            lastTouchEndedIndex = GetLastTouchIndex(TouchPhase.Canceled, out fingerId);
        if (lastTouchEndedIndex != -1 && m_currentlyPressedId == fingerId)
        {
            m_currentlyPressedId  = -1;

            Vector2 touchPosition = Input.GetTouch(lastTouchEndedIndex).position;

            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(touchPosition);
            if (viewportPoint.x < 0.5f)
            {
                OnLeftButtonReleased();
            }
            else
            {
                OnRightButtonReleased();
            }
        }
    }

    private int GetLastTouchIndex(TouchPhase phase, out int fingerId)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == phase)
            {
                fingerId = Input.GetTouch(i).fingerId;
                return i;
            }
        }

        fingerId = -1;
        return -1;
    }
}
