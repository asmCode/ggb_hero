using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public UISprite m_fill;

    private UIProgressBar m_progressBar;

    public void SetHealth(float health)
    {
        if (health < 0.3f)
        {
            m_fill.color = Color.red;
        }
        else
        {
            m_fill.color = Color.green;
        }

        m_progressBar.value = health;
    }

    private void Awake()
    {
        m_progressBar = GetComponent<UIProgressBar>();
    }
}
