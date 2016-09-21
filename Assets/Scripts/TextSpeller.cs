using UnityEngine;
using System.Collections;

public class TextSpeller : MonoBehaviour
{
    public float m_lettersPerSecond;
    public bool m_startSpell;

    private UILabel m_label;
    private string m_text;
    private bool m_isSpelling;

    public void StartSpelling()
    {
        StartCoroutine(SpellingCoroutine());
    }

    private void Awake()
    {
        m_isSpelling = false;
        m_startSpell = false;
        m_label = GetComponent<UILabel>();
        m_text = m_label.text;
        m_label.text = "";
    }

    private void Update()
    {
        if (m_startSpell && !m_isSpelling)
        {
            m_isSpelling = true;
            StartSpelling();
        }
    }

    private IEnumerator SpellingCoroutine()
    {
        float delay = 1.0f / m_lettersPerSecond;

        int letterIndex = 0;

        while (letterIndex != m_text.Length)
        {
            letterIndex++;
            m_label.text = m_text.Substring(0, letterIndex);

            yield return new WaitForSeconds(delay);
        }
    }
}
