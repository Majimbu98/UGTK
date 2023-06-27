// © 2023 Marcello De Bonis. All rights reserved.

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    //Scriptable container where T is the content type
    public abstract class S_Container_Data<T>: S_Container<Data<T>>, IInspectable
    {
        public virtual void IOnInspectorGUIButton()
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

        public void IOnInspectorGUIVariable()
        {
            
        }

        public virtual void CloneDefaultIntoCurrent() { }
    }
}


