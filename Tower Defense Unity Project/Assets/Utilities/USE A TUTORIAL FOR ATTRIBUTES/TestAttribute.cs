using UnityEngine;
using UnityEditor;

/// Any property attribute we create should ideally be suffixed '-Attribute'.
/// All property attributes must derive from 'PropertyAttribute'. 
public class TestAttribute : PropertyAttribute
{
    /// This header tells the drawer class 'TestDrawer' which class or PropertyAttribute it's a drawer for. 
    [CustomPropertyDrawer(typeof(TestAttribute))]
    public class TestDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = true;
            EditorGUI.Slider(position, property, TestAttributeTest._redMin, 1, label);

            
        }
    }
}
