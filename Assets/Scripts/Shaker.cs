using UnityEngine;
using System.Collections;

public class Shaker
{
	private float m_shakeRange;
	private float m_shakeSpeed;

	public float ShakeRange
	{
		get { return m_shakeRange; }
		set
		{
			m_shakeRange = value;

			//CalcNextTarget();
		}
	}

	public float ShakeSpeed
	{
		get { return m_shakeSpeed; }
		set
		{
			m_shakeSpeed = value;
		}
	}

	private Vector3 m_nextTarget;
	private Vector3 m_currPosition;
	private float m_time;

	public Shaker(float shakeRange, float shakeSpeed)
	{
		m_shakeRange = shakeRange;
		m_shakeSpeed = shakeSpeed;

		CalcNextTarget();
	}

	public Vector3 GetShakeValue(float deltaTime)
	{
		m_time += deltaTime * ShakeSpeed;

		Vector3 shakeValue = Vector3.Lerp(m_currPosition, m_nextTarget, m_time);

		if (m_time >= 1.0f)
		{
			m_currPosition = m_nextTarget;
			CalcNextTarget();
		}

		return shakeValue;
	}

	private void CalcNextTarget()
	{
        var random = new System.Random();
		m_nextTarget.x = (float)random.NextDouble() * ShakeRange;
        m_nextTarget.y = (float)random.NextDouble() * ShakeRange;
        m_nextTarget.z = (float)random.NextDouble() * ShakeRange;

        m_time = 0.0f;
	}
}
