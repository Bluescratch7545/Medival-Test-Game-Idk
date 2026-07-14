using System;
using UnityEngine;

[Serializable]
public class uFSMValue
{
    public enum ValueType
    {
        Int,
        Float,
        Bool,
        String,
        Object
    }

    public ValueType type;

    public int intValue;
    public float floatValue;
    public bool boolValue;
    public string stringValue;
    public UnityEngine.Object objectValue;

    public object Get()
    {
        return type switch
        {
            ValueType.Int => intValue,
            ValueType.Float => floatValue,
            ValueType.Bool => boolValue,
            ValueType.String => stringValue,
            ValueType.Object => objectValue,
            _ => null
        };
    }

    public void Set(object value)
    {
        switch (value)
        {
            case int i:
                type = ValueType.Int;
                intValue = i;
                break;

            case float f:
                type = ValueType.Float;
                floatValue = f;
                break;

            case bool b:
                type = ValueType.Bool;
                boolValue = b;
                break;

            case string s:
                type = ValueType.String;
                stringValue = s;
                break;

            case UnityEngine.Object o:
                type = ValueType.Object;
                objectValue = o;
                break;
        }
    }
}