using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    public Transform m_hand;
    public Transform m_suiContainer;
    public RectBounds m_superheroArea;

    public int GetHoldingSuis()
    {
        return m_suiContainer.childCount;
    }

    void Update()
    {
        Bounds bounds = m_superheroArea.GetBounds();

        Vector3 position = transform.position;

        if (position.x < bounds.min.x)
        {
            position.x = bounds.min.x;
        }
        if (position.x > bounds.max.x)
        {
            position.x = bounds.max.x;
        }
        if (position.y > bounds.max.y)
        {
            position.y = bounds.max.y;
        }
        if (position.y < bounds.min.y)
        {
            position.y = bounds.min.y;
        }

        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Suicider suicider = other.gameObject.GetComponent<Suicider>();
        if (suicider != null)
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
        sui.GetComponent<Collider>().enabled = false;
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
