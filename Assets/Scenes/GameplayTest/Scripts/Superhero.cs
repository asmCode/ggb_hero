using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    public Transform m_hand;

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
        if (!IsFree())
            return;

        Suicider suicider = other.gameObject.GetComponent<Suicider>();

        other.gameObject.transform.parent = m_hand;
        other.transform.localPosition = Vector3.zero;
        suicider.StopFalling();
    }

    private bool IsFree()
    {
        return m_hand.childCount == 0;
    }

    private void ReleaseSuiciders()
    {
        DestroyObject(m_hand.GetChild(0).gameObject);
    }
}
