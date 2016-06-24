using UnityEngine;
using System.Collections;

public class ColorPalette : MonoBehaviour
{
    public Color[] m_colors;

    private static ColorPalette m_thisStatic;

    public static Color GetRandomColor()
    {
        return m_thisStatic.m_colors[Random.Range(0, m_thisStatic.m_colors.Length)];
    }

    private void Awake()
    {
        m_thisStatic = this;
    }
}
