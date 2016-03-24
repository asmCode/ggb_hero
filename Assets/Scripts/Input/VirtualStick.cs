using UnityEngine;
using System.Collections;

public class VirtualStick : Stick
{
    private bool m_isMoving;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Origin = new Vector2(viewportPoint.x, viewportPoint.y);
            m_isMoving = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_isMoving = false;
            Value = Vector2.zero;
        }

        if (Input.GetMouseButton(0) && m_isMoving)
        {
            Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector2 viewportPoint2d = new Vector2(viewportPoint.x, viewportPoint.y);

            Vector2 value = (viewportPoint2d - Origin) / Range;
            value.x *= Camera.main.aspect;
            if (value.magnitude > 1.0f)
            {
                value.Normalize();
            }

            Value = value;
        }

        //Debug.Log(Value.x.ToString("0.00") + " " + Value.y.ToString("0.00"));
    }
}
