using UnityEngine;
using System.Collections;

public class StickView : MonoBehaviour
{
    public Stick m_stick;
    public UISprite m_originSprite;
    public UISprite m_rangeSprite;
    public Camera m_rootCamera;

    void Update()
    {
        Vector2 origin = m_stick.Origin;
        origin -= new Vector2(0.5f, 0.5f);
        Vector2 stickPosition = origin + new Vector2(m_stick.Value.x / m_rootCamera.aspect, m_stick.Value.y) * m_stick.Range;
        Vector3 originScreenPoint = m_rootCamera.ViewportToScreenPoint(origin);
        Vector3 stickScreenPoint = m_rootCamera.ViewportToScreenPoint(stickPosition);

        m_originSprite.transform.localPosition = stickScreenPoint;

        Vector3 rangeSize = m_rootCamera.ViewportToScreenPoint(new Vector2((m_stick.Range) / m_rootCamera.aspect, m_stick.Range));
        m_rangeSprite.width = 2;
        m_rangeSprite.height = 2;
        m_rangeSprite.transform.localScale = rangeSize;
        m_rangeSprite.transform.localPosition = originScreenPoint;
    }
}
