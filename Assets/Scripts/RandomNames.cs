using UnityEngine;
using System.Collections;

public class RandomNames
{
    private string[] m_maleNames;
    private string[] m_femaleNames;

    public void Initialize()
    {
        m_maleNames = LoadNames("male_names");
        m_femaleNames = LoadNames("female_names");
    }

    public string GetName(bool female)
    {
        string[] names = female ? m_femaleNames : m_maleNames;

        if (names == null || names.Length == 0)
            return null;

        return names[Random.Range(0, names.Length)];
    }

    private string[] LoadNames(string assetName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(assetName);
        if (textAsset == null || textAsset.text == null)
            return null;

        return textAsset.text.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);
    }
}
