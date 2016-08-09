using UnityEngine;
using System.Collections;

public class WaterPhysics : MonoBehaviour
{
    public float m_angularFrequency = 5.0f;
    public float m_dampingRatio = 0.2f;

    private float m_omegaZeta;
    private float m_alpha;
    private float[] m_heights;
    private float[] m_speeds;

    public void Init(float[] heights)
    {
        m_heights = heights;

        if (heights == null)
        {
            m_speeds = null;
            return;
        }

        m_speeds = new float[m_heights.Length];

        for (int i = 0; i < m_heights.Length; i++)
        {
            //m_heights[i] = Mathf.Sin(i / 10.0f) * 0.8f + 0.2f;
        }
    }

    public void AddSpeed(int index, float speed)
    {
        m_speeds[index] = (m_speeds[index] + speed) / 2.0f;
    }

    private void Awake()
    {
        m_omegaZeta = m_angularFrequency * m_dampingRatio;
        m_alpha = m_angularFrequency * Mathf.Sqrt(1.0f - m_dampingRatio * m_dampingRatio);
    }

    private void Update()
    {
        if (m_heights == null || m_speeds == null)
            return;

        float deltaTime = Time.deltaTime;

        float expTerm = Mathf.Exp(-m_omegaZeta * deltaTime);
        float cosTerm = Mathf.Cos(m_alpha * deltaTime);
        float sinTerm = Mathf.Sin(m_alpha * deltaTime);

        for (int i = 0; i < m_heights.Length; i++)
        {
            CalcDampedSimpleHarmonicMotion(ref m_heights[i], ref m_speeds[i], deltaTime, expTerm, cosTerm, sinTerm);
        }
    }

    private void CalcDampedSimpleHarmonicMotion(
        ref float pPos,
        ref float pVel,
        float deltaTime,
        float expTerm,
        float cosTerm,
        float sinTerm)
    {
        float c1 = pPos;
        float c2 = (pVel + m_omegaZeta * c1) / m_alpha;
        pPos =  expTerm * (c1 * cosTerm + c2 * sinTerm);
        pVel = -expTerm * ((c1 * m_omegaZeta - c2 * m_alpha) * cosTerm +
                           (c1 * m_alpha + c2 * m_omegaZeta) * sinTerm);
    }
}
