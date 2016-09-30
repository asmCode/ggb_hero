using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Water : MonoBehaviour
{
    #region Inspector
    public int m_segmentsCount = 128;
    public int m_constantImpulsesCount = 10;
    public Vector2 m_constantImpulsesSpeed = new Vector2(5, 15);
    public Vector2 m_constantImpulsesPower = new Vector2(0.1f, 0.4f);
    #endregion

    private float[] m_heights;
    private WaterRenderer m_waterRenderer;
    private WaterPhysics m_waterPhysics;
    private Vector3 m_localScale;

    private List<WaterImpulse> m_waterImpulses = new List<WaterImpulse>();

    public void Init()
    {
        m_localScale = transform.localScale;

        Clear();
        CreateConstantImpulses();
    }

    public int GetWaterStripIndex(float xCoord)
    {
        // Assuming that water is horizontally centered (transform.x = 0)
        float waterMinXCoord= -m_localScale.x / 1;

        float horiCoordNormalized = (xCoord - waterMinXCoord) / (m_localScale.x * 2);
        horiCoordNormalized = Mathf.Clamp01(horiCoordNormalized);
        return Mathf.Clamp((int)(horiCoordNormalized * m_segmentsCount), 0, m_segmentsCount - 1);
    }

    public float GetWaterHeight(int stripIndex)
    {
        float height = transform.position.y;
        height += m_heights[stripIndex];
        return height;
    }

    public void Impulse(int stripIndex, float power, float xCoord)
    {
        WaterImpulse impulse = new WaterImpulse(m_segmentsCount, power, 40.0f, 0.04f, stripIndex - 1, -1);
        m_waterImpulses.Add(impulse);
        impulse = new WaterImpulse(m_segmentsCount, power, 40.0f, 0.04f, stripIndex + 1, 1);
        m_waterImpulses.Add(impulse);
        m_waterPhysics.AddSpeed(stripIndex, -power * 1.01f);

        CreateSplash(power * 1.2f, stripIndex, xCoord);
    }

    private void CreateSplash(float speed, int stripIndex, float xCoord)
    {
        WaterSplash splash = WaterSplashPool.Instance.Get();
        if (splash == null)
           return;
        splash.Splash(speed * 0.5f, Vector2.up, GetWaterHeight(stripIndex), xCoord);
    }

    private void CreateConstantImpulses()
    {
        m_waterImpulses.Clear();

        for (int i = 0; i < m_constantImpulsesCount; i++)
        {
            float power = Random.Range(m_constantImpulsesPower.x, m_constantImpulsesPower.y) * 3.0f;
            float speed = Random.Range(m_constantImpulsesSpeed.x, m_constantImpulsesSpeed.y);
            int index = Random.Range(0, m_segmentsCount);
            int direction = (index % 2 == 0) ? -1 : 1;
            WaterImpulse impulse = new WaterImpulse(m_segmentsCount, power, speed, 0.0f, index, direction);
            m_waterImpulses.Add(impulse);
        }
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    int index = GetWaterStripIndex(world.x);
        //    Impulse(index, 4.0f);
        //}

        m_waterImpulses.RemoveAll((t) => { return t.Power == 0; });

        float deltaTime = Time.deltaTime;

        foreach (var impulse in m_waterImpulses)
        {
            impulse.Update(deltaTime);

            int index;
            float power;
            while (impulse.GetImpulse(out index, out power))
                m_waterPhysics.AddSpeed(index, -power);
        }
    }

    void LateUpdate()
    {
        //if (!Input.GetKey(KeyCode.S))
        //{
        //return;
        //}
        /*
        for (int i = 0; i < m_waterStrips.Count; i++)
        {
            m_currentHeights[i] = m_waterStrips[i].transform.position.y;
        }

        float nWage = 0.2f;
        float c1Wage = 1.0f - nWage;
        float c2Wage = 1.0f - 2 * nWage;

        for (int i = 0; i < m_waterStrips.Count; i++)
        {
            Vector3 position = m_waterStrips[i].transform.position;

            if (i == 0)
            {
                position.y = m_currentHeights[0] * c1Wage + m_currentHeights[1] * nWage;
            }
            else if (i == m_waterStrips.Count - 1)
            {
                position.y = m_currentHeights[m_waterStrips.Count - 2] * nWage + m_currentHeights[m_waterStrips.Count - 1] * c1Wage;
            }
            else
            {
                position.y =
                    nWage * m_currentHeights[i - 1] +
                    c2Wage * m_currentHeights[i] +
                    nWage * m_currentHeights[i + 1];
            }

            m_waterStrips[i].transform.position = position;
        }

        */
    }

    private void Clear()
    {
        m_waterImpulses.Clear();
    }

    private void Awake()
    {
        m_waterRenderer = GetComponent<WaterRenderer>();
        m_waterPhysics = GetComponent<WaterPhysics>();

        m_heights = new float[m_segmentsCount];
        m_waterPhysics.Init(m_heights);
        m_waterRenderer.Init(m_heights);

        Init();
    }

    private void ConstantImpulsesCountWrapChanged()
    {
        CreateConstantImpulses();
    }
    void OnDrawGizmos()
    {
        return;
        //if (Application.isPlaying)
        //    return;

        //Water water = this;
        //Vector3 pos = water.transform.position;
        //pos.z = -water.m_sortingOrder;
        //Vector3 scl = water.transform.localScale;
        //scl.z = 0.01f;
        //float hx = scl.x / 2.0f;
        //float hy = scl.y / 2.0f;

        //Gizmos.color = water.m_color;
        //Gizmos.DrawCube(pos, scl);



        //Gizmos.DrawPolyLine(
        //    new Vector3(pos.x - hx, pos.y - hy, 0),
        //    new Vector3(pos.x + hx, pos.y - hy, 0),
        //    new Vector3(pos.x + hx, pos.y + hy, 0),
        //    new Vector3(pos.x - hx, pos.y + hy, 0),
        //    new Vector3(pos.x - hx, pos.y - hy, 0));
    }
}

