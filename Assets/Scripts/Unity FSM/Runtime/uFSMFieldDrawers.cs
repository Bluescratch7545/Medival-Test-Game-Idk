using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class uFSMFieldDrawers
{
    public static object DrawValue(string label, uFSMValue value)
    {

        EditorGUILayout.BeginHorizontal();

        value.type = (uFSMValue.ValueType)
            EditorGUILayout.EnumPopup(value.type, GUILayout.Width(80));

        switch (value.type)
        {
            case uFSMValue.ValueType.Int:
                value.intValue = EditorGUILayout.IntField(label, value.intValue);
                break;

            case uFSMValue.ValueType.Float:
                value.floatValue = EditorGUILayout.FloatField(label, value.floatValue);
                break;

            case uFSMValue.ValueType.Bool:
                value.boolValue = EditorGUILayout.Toggle(label, value.boolValue);
                break;

            case uFSMValue.ValueType.String:
                value.stringValue = EditorGUILayout.TextField(label, value.stringValue);
                break;

            case uFSMValue.ValueType.Object:
                value.objectValue = EditorGUILayout.ObjectField(
                    label,
                    value.objectValue,
                    typeof(UnityEngine.Object),
                    true
                );
                break;
        }

        EditorGUILayout.EndHorizontal();

        return value;
    }
    public static object DrawField(string label, Type type, object value)
    {
        if (type == typeof(int))
            return EditorGUILayout.IntField(label, value != null ? (int)value : 0);

        if (type == typeof(float))
            return EditorGUILayout.FloatField(label, value != null ? (float)value : 0f);

        if (type == typeof(bool))
            return EditorGUILayout.Toggle(label, value != null && (bool)value);

        if (type == typeof(string))
            return EditorGUILayout.TextField(label, value as string ?? "");

        if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            return EditorGUILayout.ObjectField(label, value as UnityEngine.Object, type, true);

        EditorGUILayout.LabelField(label, $"Unsupported: {type.Name}");
        return value;
    }
}