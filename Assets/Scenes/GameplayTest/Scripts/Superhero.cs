using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    public Transform m_hand;
    public Transform m_suiContainer;
    public RectBounds m_superheroArea;

    public Vector2 Velocity
    {
        get;
        set;
    }

    public int GetHoldingSuis()
    {
        return m_suiContainer.childCount;
    }

    void Update()
    {
        Bounds bounds = m_superheroArea.GetBounds();

        Vector2 position = transform.position;
        Vector2 velocity = Velocity;
        position += velocity * Time.deltaTime;

        float bounce_power = 0.3f;

        if (position.x < bounds.min.x)
        {
            position.x = bounds.min.x;
            velocity.x = -velocity.x * bounce_power;
        }
        if (position.x > bounds.max.x)
        {
            position.x = bounds.max.x;
            velocity.x = -velocity.x * bounce_power;
        }
        if (position.y > bounds.max.y)
        {
            position.y = bounds.max.y;
            velocity.y = 0.0f;
        }
        if (position.y < bounds.min.y)
        {
            position.y = bounds.min.y;
            velocity.y = 0.0f;
            velocity.x = 0.0f;
        }

        Velocity = velocity;
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Suicider suicider = other.gameObject.GetComponent<Suicider>();
        if (suicider != null && suicider.IsGrabable)
        {
            if (CanTakeSui())
            {
                AddSui(suicider);
                return;
            }
        }

        RescueArea rescueArea = other.gameObject.GetComponent<RescueArea>();
        if (rescueArea != null)
        {
            if (!IsFree())
                ReleaseSuiciders();

            return;
        }
    }

    private bool IsFree()
    {
        return m_suiContainer.childCount == 0;
    }

    private bool CanTakeSui()
    {
        return m_suiContainer.childCount < GameSettings.HandCapacity;
    }

    private void AddSui(Suicider sui)
    {
        Vector3 suiDirection = (sui.transform.position - transform.position).normalized * 0.11f;

        sui.gameObject.transform.parent = m_suiContainer;
        sui.transform.position = transform.position + suiDirection;
        sui.SetController(new SuiControllerWithSuperhero(sui));
    }

    private void ReleaseSuiciders()
    {
        GameSettings.SuiRescuedCount += m_suiContainer.childCount;

        Vector2 rescuePosition = transform.position.x < 0.0f ?
            GameObject.Find("RescuePointLeft").transform.position :
            GameObject.Find("RescuePointRight").transform.position;

        while (m_suiContainer.childCount > 0)
        {
            Transform child = m_suiContainer.GetChild(0);
            child.transform.parent = null;
            child.GetComponent<Suicider>().SetController(
                new SuiControllerRescuing(child.GetComponent<Suicider>(), rescuePosition));
        }
    }
}
