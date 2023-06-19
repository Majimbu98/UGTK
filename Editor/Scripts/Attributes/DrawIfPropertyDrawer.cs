// © 2023 Marcello De Bonis. All rights reserved.

using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using UnityGamesToolkit.Runtime;

namespace UnityGamesToolkit.Editor
{
    /// <summary>
    /// Custom property drawer for the DrawIfAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfPropertyDrawer : PropertyDrawer
    {
        #region Variables & Properties

        private DrawIfAttribute drawIf;
        private SerializedProperty compareField_A;
        private SerializedProperty compareField_B;
        private FieldInfo field_A;
        private FieldInfo field_B;
        private bool done_Once = false;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the height of the property based on the DrawIfAttribute conditions.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (ShowMe(property) == false && drawIf.disablingType == E_DisablingType.DontDraw)
                return 0f;

            return base.GetPropertyHeight(property, label);
        }

        /// <summary>
        /// Checks if the property should be shown or hidden based on the DrawIfAttribute conditions.
        /// </summary>
        private bool ShowMe(SerializedProperty property)
        {
            if (done_Once == false)
            {
                Find_CompareFields(property);
                done_Once = true;
            }

            switch (drawIf.comparisonType)
            {
                case E_ComparisonType.Property:
                    return CompareByProperties();
                case E_ComparisonType.Value:
                    return CompareByValue();
            }

            Debug.Log("Error! ComparisonType is undefined");
            return true;
        }

        /// <summary>
        /// Compares the values of the compared fields using their properties.
        /// </summary>
        private bool CompareByProperties()
        {
            if (compareField_A == null || compareField_B == null)
            {
                Debug.Log("Value NULLS");
                return true;
            }
            else if (((IComparable)field_A.GetValue(compareField_A.serializedObject.targetObject)).CompareTo(
                         field_B.GetValue(compareField_B.serializedObject.targetObject)) == 0)
            {
                Debug.Log("Same value");
                return true;
            }
            else
            {
                Debug.Log("Different value");
                return false;
            }
        }

        /// <summary>
        /// Compares the value of the compared field with a specified value.
        /// </summary>
        private bool CompareByValue()
        {
            if (compareField_A == null)
            {
                Debug.Log("Value NULL");
                return true;
            }

            object firstPropertyValue = null;

            switch (compareField_A.propertyType)
            {
                case SerializedPropertyType.Integer:
                    firstPropertyValue = compareField_A.intValue;
                    break;
                case SerializedPropertyType.Boolean:
                    firstPropertyValue = compareField_A.boolValue;
                    break;
                case SerializedPropertyType.Float:
                    firstPropertyValue = compareField_A.floatValue;
                    break;
                case SerializedPropertyType.String:
                    firstPropertyValue = compareField_A.stringValue;
                    break;
                case SerializedPropertyType.ObjectReference:
                    firstPropertyValue = compareField_A.objectReferenceValue;
                    break;
            }

            bool valuesMatch = firstPropertyValue.Equals(drawIf.compareValue);

            if (valuesMatch)
            {
                Debug.Log("Same value");
                return true;
            }
            else
            {
                Debug.Log("Different value");
                return false;
            }
        }

        /// <summary>
        /// Handles the GUI drawing for the property based on the DrawIfAttribute conditions.
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShowMe(property))
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else if (drawIf.disablingType == E_DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
        }

        /// <summary>
        /// Finds and sets the compared fields based on the DrawIfAttribute properties.
        /// </summary>
        public void Find_CompareFields(SerializedProperty property)
        {
            drawIf = attribute as DrawIfAttribute;

            string path = "";

            if (property.propertyPath.Contains(".") && property.depth == 0)
                path = System.IO.Path.ChangeExtension(property.propertyPath, drawIf.firstProperty);
            else
                path = drawIf.firstProperty; //Inside the same script

            compareField_A = property.serializedObject.FindProperty(path);

            if (compareField_A == null)
            {
                Debug.LogError("Cannot find property with name: " + path);
            }

            if (property.propertyPath.Contains(".") && property.depth == 0)
                path = System.IO.Path.ChangeExtension(property.propertyPath, drawIf.secondProperty);
            else
                path = drawIf.secondProperty;

            compareField_B = property.serializedObject.FindProperty(path);

            if (compareField_B == null)
            {
                Debug.Log("Cannot find property with name: " + path);
                return;
            }

            FieldInfo[] fields = compareField_A.serializedObject.targetObject.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                if (field.Name == compareField_A.name)
                {
                    Debug.Log(field.Name + "  .  " + field.GetValue(compareField_A.serializedObject.targetObject));
                    field_A = field;
                }
                else if (field.Name == compareField_B.name)
                {
                    Debug.Log(field.Name + "  .  " + field.GetValue(compareField_B.serializedObject.targetObject));
                    field_B = field;
                }
            }
        }
        
        #endregion
    }
}