using UnityEngine;

class FpsCounterView : MonoBehaviour
{
    public UILabel m_label = null;
    public Color m_colorGood = Color.white;
    public Color m_colorAverage = Color.white;
    public Color m_colorBad = Color.white;
    public FpsCounter m_fpsCounter = null;

    private void Update()
    {
        m_label.color = m_colorBad;
        if (m_fpsCounter.Fps > 40)
        {
            m_label.color = m_colorGood;
        }
        else if (m_fpsCounter.Fps > 20)
        {
            m_label.color = m_colorAverage;
        }
    
        m_label.text = "FPS: " + ((int)m_fpsCounter.Fps).ToString();
    }
}
