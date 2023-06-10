using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
///Implementation of the Property Attribute.
/// </summary>
[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    ShowIfAttribute showIf;

    // Field that is being compared.
    SerializedProperty comparedField;
    SerializedProperty compareTo;
    SerializedProperty similarTo;
    #endregion

    /// <summary>
    /// Specify how tall the GUI for this field is in pixels.
    /// </summary>
    /// <param name="property">The target property</param>
    /// <param name="label">The label of the field (name of the property)</param>
    /// <returns>The height of the property in pixel.</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //If the condition to show the field is not met and we don't want to see the Property 
        if (!ShowMe(property) && showIf.disablingType == DisablingType.DONTSHOW)
            return 0f;//we draw a property with pixel in heigth

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool ShowMe(SerializedProperty property)
    {
        showIf = attribute as ShowIfAttribute;

        string path = "";

        // Searching for the property specified inside the comparedPropertyName field (DrawIfAttribute)
        if (property.propertyPath.Contains("."))
            path = System.IO.Path.ChangeExtension(property.propertyPath, showIf.comparedPropertyName);//In other script
        else
            path = showIf.comparedPropertyName;//Inside the same script

        //Once the path of the compared property is defined we are basically going to get it
        comparedField = property.serializedObject.FindProperty(path);

        if (property.propertyPath.Contains("."))
            path = System.IO.Path.ChangeExtension(property.propertyPath, showIf.comparedValue);
        else
            path = showIf.comparedValue;
            
        compareTo = property.serializedObject.FindProperty(path);

        path = showIf.similarValue;
        similarTo = property.serializedObject.FindProperty(path);

        if (comparedField == null)//If the property is not found (maybe misstyping)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        if(comparedField.propertyType != compareTo.propertyType)
        {
            Debug.LogError("The variables are not the same!");
            return true;
        }

        bool returnBool = true;

        SerializedPropertyType propType = comparedField.propertyType;
        // Get the value & compare based on types
        switch (propType)
        { 
            case SerializedPropertyType.Integer:
                switch (showIf.comparisonType)
                {
                    case ComparisonType.EQUALS:
                        return comparedField.intValue.Equals(compareTo.intValue);
                    case ComparisonType.NOT_EQUALS:
                        return !comparedField.intValue.Equals(compareTo.intValue);
                    case ComparisonType.GREATER:
                        return comparedField.intValue.CompareTo(compareTo.intValue) > 0;
                    case ComparisonType.LESSER:
                        return comparedField.intValue.CompareTo(compareTo.intValue) < 0;
                    case ComparisonType.EQUALS_GREATER:
                        return comparedField.intValue.CompareTo(compareTo.intValue) >= 0;
                    case ComparisonType.EQUALS_LESSER:
                        return comparedField.intValue.CompareTo(compareTo.intValue) <= 0;
                    case ComparisonType.SIMILAR_VALUE:
                        return comparedField.intValue.CompareTo(compareTo.intValue - similarTo.intValue) >= 0 && comparedField.intValue.CompareTo(compareTo.intValue + similarTo.intValue) <= 0;

                    default:
                        Debug.LogError("Error: " + showIf.comparisonType + " is not supported of " + path);
                        return true;
                }
            case SerializedPropertyType.Float:
                switch (showIf.comparisonType)
                {
                    case ComparisonType.EQUALS:
                        return comparedField.floatValue.Equals(compareTo.floatValue);
                    case ComparisonType.NOT_EQUALS:
                        return !comparedField.floatValue.Equals(compareTo.floatValue);
                    case ComparisonType.GREATER:
                        return comparedField.floatValue.CompareTo(compareTo.floatValue) > 0;
                    case ComparisonType.LESSER:
                        return comparedField.floatValue.CompareTo(compareTo.floatValue) < 0;
                    case ComparisonType.EQUALS_GREATER:
                        return comparedField.floatValue.CompareTo(compareTo.floatValue) >= 0;
                    case ComparisonType.EQUALS_LESSER:
                        return comparedField.floatValue.CompareTo(compareTo.floatValue) <= 0;
                    case ComparisonType.SIMILAR_VALUE:
                        return comparedField.floatValue.CompareTo(compareTo.floatValue - similarTo.floatValue) >= 0 && comparedField.floatValue.CompareTo(compareTo.floatValue + similarTo.floatValue) <= 0;

                    default:
                        Debug.LogError("Error: " + showIf.comparisonType + " is not supported of " + path);
                        return true;
                }
            case SerializedPropertyType.Vector3:
                switch(showIf.comparisonType)
                {
                    case ComparisonType.EQUALS:
                        return comparedField.vector3Value.Equals(compareTo.vector3Value);
                    case ComparisonType.NOT_EQUALS:
                        return !comparedField.vector3Value.Equals(compareTo.vector3Value);
                    case ComparisonType.SIMILAR_VALUE:
                        return (comparedField.vector3Value.x.CompareTo(compareTo.vector3Value.x - similarTo.floatValue) >= 0 && comparedField.vector3Value.x.CompareTo(compareTo.vector3Value.x + similarTo.floatValue) <= 0) &&
                               (comparedField.vector3Value.y.CompareTo(compareTo.vector3Value.y - similarTo.floatValue) >= 0 && comparedField.vector3Value.y.CompareTo(compareTo.vector3Value.y + similarTo.floatValue) <= 0) &&
                               (comparedField.vector3Value.z.CompareTo(compareTo.vector3Value.z - similarTo.floatValue) >= 0 && comparedField.vector3Value.z.CompareTo(compareTo.vector3Value.z + similarTo.floatValue) <= 0);

                    default:
                        Debug.LogError("Error: " + showIf.comparisonType + " is not supported of " + path);
                        return true;
                }
            case SerializedPropertyType.Vector2:
                switch (showIf.comparisonType)
                {
                    case ComparisonType.EQUALS:
                        return comparedField.vector2Value.Equals(compareTo.vector2Value);
                    case ComparisonType.NOT_EQUALS:
                        return !comparedField.vector2Value.Equals(compareTo.vector2Value);
                    case ComparisonType.SIMILAR_VALUE:
                        return (comparedField.vector2Value.x.CompareTo(compareTo.vector2Value.x - similarTo.floatValue) >= 0 && comparedField.vector2Value.x.CompareTo(compareTo.vector2Value.x + similarTo.floatValue) <= 0) &&
                               (comparedField.vector2Value.y.CompareTo(compareTo.vector2Value.y - similarTo.floatValue) >= 0 && comparedField.vector2Value.y.CompareTo(compareTo.vector2Value.y + similarTo.floatValue) <= 0);
                              
                    default:
                        Debug.LogError("Error: " + showIf.comparisonType + " is not supported of " + path);
                        return true;
                }
            case SerializedPropertyType.ObjectReference:
                returnBool = comparedField.objectReferenceInstanceIDValue.Equals(compareTo.objectReferenceInstanceIDValue);
                break;

            default:
                var fieldOne = comparedField.serializedObject.targetObject.GetType().GetField(comparedField.name);
                var fieldTwo = compareTo.serializedObject.targetObject.GetType().GetField(compareTo.name);
                if(fieldOne == null || fieldTwo == null)
                {
                    string temp = fieldOne == null ? comparedField.displayName : compareTo.displayName;
                    Debug.LogError(temp + " is not accesible (check if variable is public)");
                    return true;
                }
                var valueOne = fieldOne.GetValue(comparedField.serializedObject.targetObject);
                var valueTwo = fieldTwo.GetValue(compareTo.serializedObject.targetObject);
                returnBool = valueOne.Equals(valueTwo);
                break;
        }

        if (showIf.comparisonType == ComparisonType.EQUALS)
            return returnBool;
        else if (showIf.comparisonType == ComparisonType.NOT_EQUALS)
            return !returnBool;

        return true;
    }

    /// <summary>
    /// Is the method that allows us to change the Inspector.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property, label);
        }
        //...check if the disabling type is read only. If it is, draw it disabled
        else if (showIf.disablingType == DisablingType.READONLY)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}