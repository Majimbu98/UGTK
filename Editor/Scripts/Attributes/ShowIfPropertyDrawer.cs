// © 2023 Marcello De Bonis. All rights reserved.

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{
    /// <summary>
    /// Custom property drawer for the ShowIfAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        #region Variables & Properties

        private ShowIfAttribute showIf;

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the height of the property field based on the ShowIf condition.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ShowMe(property) && showIf.disablingType == E_DisablingType.DONTSHOW)
                return 0f;

            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Determines whether the property should be shown based on the ShowIf condition.
        /// </summary>
        private bool ShowMe(SerializedProperty property)
        {
            showIf = attribute as ShowIfAttribute;

            MonoBehaviour script = property.serializedObject.targetObject as MonoBehaviour;

            if (script != null)
            {
                Type scriptType = script.GetType();

               /* MethodInfo method = scriptType.GetMethod(showIf.boolMethod);

                if (method != null && method.ReturnType == typeof(bool) && method.GetParameters().Length == 0)
                {
                    bool result = (bool)method.Invoke(script, null);
                    return result;
                }
                */
            }
            else
            {
                Debug.LogError("Error: " + script.name + " isn't a MonoBehaviour Script");
            }

            return true;
        }

        /// <summary>
        /// Draws the property field in the inspector, respecting the ShowIf condition.
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShowMe(property))
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else if (showIf.disablingType == E_DisablingType.READONLY)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
        }

        #endregion
    }
}