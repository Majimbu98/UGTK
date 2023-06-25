// © 2023 Marcello De Bonis. All rights reserved.

using UnityEditor;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(S_Container_Data<>), true)]
    public class S_DataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            ((IInspectable)target).IOnInspectorGUI();


        }
    }

#endif
}