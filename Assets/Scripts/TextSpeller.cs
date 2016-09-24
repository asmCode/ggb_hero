using UnityEngine;
using System.Collections;
using System;

public class TextSpeller : MonoBehaviour, ISkipable
{
    public float m_lettersPerSecond;
    public bool m_startSpell;

    private UILabel m_label;
    private string m_text;
    private bool m_isSpelling;
    private bool m_isInitialized;
    private bool m_isSkipped;

    public UILabel Label
    {
        get
        {
            if (m_label == null)
                m_label = GetComponent<UILabel>();

            return m_label;
        }
    }

    public void StartSpelling()
    {
        StartCoroutine("SpellingCoroutine");
    }

    private void Init()
    {
        if (m_isInitialized)
            return;

        m_isSpelling = false;
        m_startSpell = false;
        m_text = Label.text;
        Label.text = "";

        m_isInitialized = true;
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (m_startSpell && !m_isSpelling && !m_isSkipped)
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
            Label.text = m_text.Substring(0, letterIndex);

            yield return new WaitForSeconds(delay);
        }
    }

    public void Skip()
    {
        Init();

        if (!m_startSpell)
            return;

        StopCoroutine("SpellingCoroutine");
        Label.text = m_text;
        m_isSkipped = true;
    }
}
