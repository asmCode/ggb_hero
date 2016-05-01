using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Water))]
public class WaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Water water = (Water)target;

        if (GUILayout.Button("Recalculate"))
        {
            water.CreateWater();
        }
    }
}
