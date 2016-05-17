using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour
{
    #region Inspector
    public WaterStrip m_waterStripPrefab;
    public WaterSplash m_waterSplashPrefab;
    public Transform m_waterStripContainer;
    public Color m_color = Color.blue;
    public float m_waterStripWidth = 0.1f;
    public int m_sortingOrder = 0;
    public float m_springDampingRatio = 0.2f;
    public float m_springFrequency = 1.0f;
    public int m_constantImpulsesCount = 10;
    public Vector2 m_constantImpulsesSpeed = new Vector2(5, 15);
    public Vector2 m_constantImpulsesPower = new Vector2(0.1f, 0.4f);
    #endregion

    private InspectorValue<float> m_springDampingRatioWrap;
    private InspectorValue<float> m_springFrequencyWrap;
    private InspectorValue<int> m_constantImpulsesCountWrap;

    private List<WaterStrip> m_waterStrips = new List<WaterStrip>();
    private List<WaterImpulse> m_waterImpulses = new List<WaterImpulse>();

    public void CreateWater()
    {
        Clear();

        float hx = transform.localScale.x / 2;
        float hy = transform.localScale.y;

        for (float x = -hx + m_waterStripWidth / 2; x <= hx; x += m_waterStripWidth)
        {
            WaterStrip waterStrip = Instantiate(m_waterStripPrefab);
            waterStrip.transform.parent = m_waterStripContainer;
            waterStrip.transform.localPosition = new Vector3(x / transform.localScale.x, waterStrip.transform.position.y / transform.localScale.y, 0);
            waterStrip.transform.localScale = new Vector3(m_waterStripWidth / transform.localScale.x, waterStrip.transform.localScale.y * hy, 1);
            waterStrip.GetComponent<SpringJoint2D>().anchor = new Vector2(0, 0);
            waterStrip.GetComponent<SpringJoint2D>().connectedAnchor = waterStrip.transform.position;
            waterStrip.GetComponent<SpringJoint2D>().dampingRatio = m_springDampingRatio;
            waterStrip.GetComponent<SpringJoint2D>().frequency = m_springFrequency;
            waterStrip.GetComponent<SpriteRenderer>().color = m_color;
            waterStrip.GetComponent<SpriteRenderer>().sortingOrder = m_sortingOrder;
            m_waterStrips.Add(waterStrip);
        }

        CreateConstantImpulses();
    }

    public int GetWaterStripIndex(float horiCoord)
    {
        float min = m_waterStrips[0].transform.position.x;
        float max = m_waterStrips[m_waterStrips.Count - 1].transform.position.x;
        horiCoord = Mathf.Clamp(horiCoord, min, max);

        float horiCoordNormalized = (horiCoord - min) / (max - min);
        return (int)(horiCoordNormalized * m_waterStrips.Count);
    }

    public float GetWaterHeight(int stripIndex)
    {
        return m_waterStrips[stripIndex].GetHeight();
    }

    public void Impulse(int stripIndex, float power)
    {
        WaterImpulse impulse = new WaterImpulse(power, 20.0f, 0.04f, stripIndex, -1);
        m_waterImpulses.Add(impulse);
        impulse = new WaterImpulse(power, 20.0f, 0.04f, stripIndex, 1);
        m_waterImpulses.Add(impulse);

        CreateSplash(power * 1.2f, stripIndex);
    }

    private void CreateSplash(float speed, int stripIndex)
    {
        WaterStrip waterStrip = m_waterStrips[stripIndex];
        WaterSplash splash = Instantiate(m_waterSplashPrefab);
        splash.Splash(speed, Vector2.up, waterStrip);
    }

    private void CreateConstantImpulses()
    {
        m_waterImpulses.Clear();

        for (int i = 0; i < m_constantImpulsesCount; i++)
        {
            float power = Random.Range(m_constantImpulsesPower.x, m_constantImpulsesPower.y) * 3.0f;
            float speed = Random.Range(m_constantImpulsesSpeed.x, m_constantImpulsesSpeed.y);
            int index = Random.Range(0, m_waterStrips.Count);
            int direction = (index % 2 == 0) ? -1 : 1;
            WaterImpulse impulse = new WaterImpulse(power, speed, 0.0f, index, direction);
            m_waterImpulses.Add(impulse);
        }
    }

    void Update()
    {
        m_springDampingRatioWrap.Check();
        m_springFrequencyWrap.Check();
        m_constantImpulsesCountWrap.Check();

        m_waterImpulses.RemoveAll((t) => { return t.Power == 0; });

        foreach (var impulse in m_waterImpulses)
        {
            impulse.Update(m_waterStrips);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            int index = Random.Range(1, m_waterStrips.Count - 1);
            m_waterStrips[index].GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1.6f, ForceMode2D.Impulse);
            m_waterImpulses.Add(new WaterImpulse(1.5f, 60, 0.2f, index, 1));
            m_waterImpulses.Add(new WaterImpulse(1.5f, 60, 0.2f, index, -1));
        }
    }

    private void SetSpringFrequency(float freq)
    {
        foreach (var strip in m_waterStrips)
        {
            strip.GetComponent<SpringJoint2D>().frequency = freq;
        }
    }

    private void SetSpringDamping(float damp)
    {
        foreach (var strip in m_waterStrips)
        {
            strip.GetComponent<SpringJoint2D>().dampingRatio = damp;
        }
    }

    void LateUpdate()
    {
        //if (!Input.GetKey(KeyCode.S))
        //{
        //    return;
        //}

        float[] currentHeights = new float[m_waterStrips.Count];
        for (int i = 0; i < m_waterStrips.Count; i++)
        {
            currentHeights[i] = m_waterStrips[i].transform.position.y;
        }

        float nWage = 0.2f;
        float c1Wage = 1.0f - nWage;
        float c2Wage = 1.0f - 2 * nWage;

        for (int i = 0; i < m_waterStrips.Count; i++)
        {
            Vector3 position = m_waterStrips[i].transform.position;

            if (i == 0)
            {
                position.y = currentHeights[0] * c1Wage + currentHeights[1] * nWage;
            }
            else if (i == m_waterStrips.Count - 1)
            {
                position.y = currentHeights[m_waterStrips.Count - 2] * nWage + currentHeights[m_waterStrips.Count - 1] * c1Wage;
            }
            else
            {
                position.y =
                    nWage * currentHeights[i - 1] +
                    c2Wage * currentHeights[i] +
                    nWage * currentHeights[i + 1];
            }

            m_waterStrips[i].transform.position = position;
        }
    }

    private void Clear()
    {
        while (m_waterStripContainer.childCount > 0)
        {
            Transform child = m_waterStripContainer.GetChild(0);
            child.parent = null;
            Destroy(child.gameObject);
        }

        m_waterImpulses.Clear();
        m_waterStrips.Clear();
    }

    private void Awake()
    {
        CreateWater();

        m_springDampingRatioWrap = new InspectorValue<float>(() => { return m_springDampingRatio; });
        m_springDampingRatioWrap.Changed += SpringDampingRatioWrapChanged;

        m_springFrequencyWrap = new InspectorValue<float>(() => { return m_springFrequency; });
        m_springFrequencyWrap.Changed += SpringFrequencyWrapChanged;

        m_constantImpulsesCountWrap = new InspectorValue<int>(() => { return m_constantImpulsesCount; });
        m_constantImpulsesCountWrap.Changed += ConstantImpulsesCountWrapChanged;
    }

    private void ConstantImpulsesCountWrapChanged()
    {
        CreateConstantImpulses();
    }

    private void SpringDampingRatioWrapChanged()
    {
        SetSpringDamping(m_springDampingRatio);
    }

    private void SpringFrequencyWrapChanged()
    {
        SetSpringFrequency(m_springFrequency);
    }

    void OnDrawGizmos()
    {
        return;
        if (Application.isPlaying)
            return;

        Water water = this;
        Vector3 pos = water.transform.position;
        pos.z = -water.m_sortingOrder;
        Vector3 scl = water.transform.localScale;
        scl.z = 0.01f;
        float hx = scl.x / 2.0f;
        float hy = scl.y / 2.0f;

        Gizmos.color = water.m_color;
        Gizmos.DrawCube(pos, scl);
        //Gizmos.DrawPolyLine(
        //    new Vector3(pos.x - hx, pos.y - hy, 0),
        //    new Vector3(pos.x + hx, pos.y - hy, 0),
        //    new Vector3(pos.x + hx, pos.y + hy, 0),
        //    new Vector3(pos.x - hx, pos.y + hy, 0),
        //    new Vector3(pos.x - hx, pos.y - hy, 0));
    }
}

