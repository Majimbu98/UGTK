using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VFXData))]
public class VFXDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VFXData vfxData = (VFXData)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Load CSV"))
        {
            string filePath = EditorUtility.OpenFilePanel("Load CSV", "", "csv");
            vfxData.Load(filePath);
        }

        if (GUILayout.Button("Save CSV"))
        {
            string filePath = EditorUtility.SaveFilePanel("Save CSV", "", "vfx_data", "csv");
            vfxData.Save(filePath);
        }
    }
}