using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && drawIf.disablingType == DrawIfAttribute.DisablingType.DontDraw)
            return 0f;

        // The height of the property should be defaulted to the default height.
        return base.GetPropertyHeight(property, label);
    }


    // Errors default to showing the property.
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;

        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

       
        switch (comparedField.type)
        { 
            case "bool":
                return comparedField.boolValue.Equals(drawIf.comparedValue);
            case "Enum":
                return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
            default:
                Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                return true;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property,label);
        } 
        else if (drawIf.disablingType == DrawIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property,label);
            GUI.enabled = true;
        }
    }

}