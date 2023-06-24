// © 2023 Marcello De Bonis. All rights reserved.

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Runtime
{
    public class S_Data<T>: S_Content<Data<T>>, IInspectable
    {
        public void IOnInspectorGUI()
        {
#if UNITY_EDITOR
            if (GUILayout.Button("Reset Default Variable"))
            { 
                content.SetToDefaultValue();
                UnityEditor.EditorWindow view = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow));
                view.Repaint();
            }
#endif
        }
    }
}
