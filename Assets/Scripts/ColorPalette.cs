using UnityEngine;
using System.Collections;

public class ColorPalette : MonoBehaviour
{
    public Color[] m_colors;
    public Color[] m_skinColors;

    private static ColorPalette m_thisStatic;

    public static Color GetRandomColor()
    {
        return m_thisStatic.m_colors[Random.Range(0, m_thisStatic.m_colors.Length)];
    }

    public static Color GetRandomSkinColor()
    {
        return m_thisStatic.m_skinColors[Random.Range(0, m_thisStatic.m_skinColors.Length)];
    }

    private void Awake()
    {
        m_thisStatic = this;
    }
}
