// © 2023 Marcello De Bonis. All rights reserved.

using UnityEngine;

namespace UnityGamesToolkit.Runtime
{
    public abstract class S_Container_Data_Content<T> : S_Container_Data<T> where T : Content<T>
    {
        public override void IOnInspectorGUIButton()
        {
#if UNITY_EDITOR
            base.IOnInspectorGUIButton();
            if (GUILayout.Button("Clone Default into Current"))
            {
                CloneDefaultIntoCurrent();
            }
#endif
        }

        public override void CloneDefaultIntoCurrent()
        {
            content.currentValue = content.defaultValue.Clone();
        }
    }
}
