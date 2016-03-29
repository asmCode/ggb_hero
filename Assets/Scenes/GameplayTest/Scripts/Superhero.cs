using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    public Transform m_hand;
    public Transform m_suiContainer;

    public int GetHoldingSuis()
    {
        return m_suiContainer.childCount;
    }

    void Update()
    {
        float hmargin = 8.5f;

        if (!IsFree() &&
            ((transform.position.x < -hmargin + 1) || (transform.position.x > hmargin - 1)))
        {
            ReleaseSuiciders();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanTakeSui())
            return;

        Suicider suicider = other.gameObject.GetComponent<Suicider>();
        if (suicider == null)
            return;

        AddSui(suicider);
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
        Vector3 suiDirection = (sui.transform.position - transform.position).normalized * 0.4f;

        sui.gameObject.transform.parent = m_suiContainer;
        sui.transform.position = transform.position + suiDirection;
        sui.StopFalling();
        sui.GetComponent<Collider>().enabled = false;
    }

    private void ReleaseSuiciders()
    {
        while (m_suiContainer.childCount > 0)
        {
            Transform child = m_suiContainer.GetChild(0);
            child.transform.parent = null;
            DestroyObject(child.gameObject);
        }
    }
}
