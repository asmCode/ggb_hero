using UnityEngine;
using System.Collections;

public class WaveIndicator : MonoBehaviour
{
    #region Inspector
    public UILabel m_labelSuiciders;
    public UILabel m_labelWave;
    public UILabel m_labelGo;
    #endregion

    private bool m_isVisible;

    public void ShowWave(int waveNumber)
    {
        if (m_isVisible)
            return;

        m_labelWave.text = string.Format("WAVE {0}", waveNumber.ToString());
        NGUITools.SetActive(m_labelWave.gameObject, true);
        NGUITools.SetActive(m_labelSuiciders.gameObject, true);
        NGUITools.SetActive(m_labelGo.gameObject, false);
        NGUITools.SetActive(gameObject, true);

        Invoke("ShowGo", 2.0f);

        m_isVisible = true;
    }

    private void ShowGo()
    {
        NGUITools.SetActive(m_labelWave.gameObject, false);
        NGUITools.SetActive(m_labelSuiciders.gameObject, false);
        NGUITools.SetActive(m_labelGo.gameObject, true);

        Invoke("Hide", 0.7f);
    }

    private void Hide()
    {
        NGUITools.SetActive(gameObject, false);

        m_isVisible = false;
    }

    private void Start()
    {
        NGUITools.SetActive(gameObject, false);
    }
}
