using UnityEngine;
using System.Collections;

public class VirtualButtons : Buttons
{
    private int m_currentlyPressedIndex = -1;

    void Update()
    {
        int lastTouchBeganIndex = GetLastTouchIndex(TouchPhase.Began);
        if (lastTouchBeganIndex != -1)
        {
            m_currentlyPressedIndex = lastTouchBeganIndex;

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

        int lastTouchEndedIndex = GetLastTouchIndex(TouchPhase.Ended);
        if (lastTouchEndedIndex != -1 && m_currentlyPressedIndex == lastTouchEndedIndex)
        {
            lastTouchEndedIndex = -1;

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

    private int GetLastTouchIndex(TouchPhase phase)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == phase)
                return i;
        }

        return -1;
    }
}
