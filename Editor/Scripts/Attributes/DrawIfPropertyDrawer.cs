

using UnityEditor;
using UnityEngine;

/// <summary>
///Implementation of the Property Attribute.
/// </summary>
[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

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
        if (!ShowMe(property) && drawIf.disablingType == DisablingType.DontDraw)
            return 0f;//we draw a property with pixel in height

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;

        string path = "";

        // Searching for the property specified inside the comparedPropertyName field (DrawIfAttribute)
        if (property.propertyPath.Contains("."))
            path = System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName);//In other script
        else
            path = drawIf.comparedPropertyName;//Inside the same script

        //Once the path of the compared property is defined we are basically going to get it
        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)//If the property is not found (maybe misstyping)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // Get the value & compare based on types
        switch (comparedField.type)
        { // Possible extend cases to support your own type
            case "bool":
                return comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
            case "vector":
                //int value = comparedField.GetArrayElementAtIndex(0).intValue;
                //return value == (int)drawIf.comparedValue;
                
            default:
                //Other type of comparison will fail (> = <)
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
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
        else if (drawIf.disablingType == DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }

}
