using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using Kaynir.Scenes;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty buildIndex = property.FindPropertyRelative("_buildIndex");

        buildIndex.intValue = EditorGUI.Popup(position, label.text, buildIndex.intValue, GetSceneOptions());

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    private string[] GetSceneOptions()
    {
        return Array.ConvertAll(EditorBuildSettings.scenes,
                                s => Path.GetFileNameWithoutExtension(s.path));
    }
}