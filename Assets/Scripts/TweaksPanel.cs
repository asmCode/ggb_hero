using UnityEngine;
using System.Collections;

public class TweaksPanel : MonoBehaviour
{
    public UILabel m_handCapacityValue;

    void Update()
    {
        m_handCapacityValue.text = GameSettings.HandCapacity.ToString();
    }

    public void HandleHandCapacityInc()
    {
        GameSettings.HandCapacity = Mathf.Clamp(GameSettings.HandCapacity + 1, 0, int.MaxValue);
    }

    public void HandleHandCapacityDec()
    {
        GameSettings.HandCapacity = Mathf.Clamp(GameSettings.HandCapacity - 1, 0, int.MaxValue);
    }
}
