using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Unity FSM/Actions/Set Bool")]
public class SetBool : uFSMAction
{
    public MonoBehaviour go;

    public string field;

    public uFSMValue value;

    public override void OnEnter(uFSMContext ctx)
    {
        SetValue(go, field, value);
    }

    static bool SetValue(MonoBehaviour script, string path, object value)
    {
        if (script == null || string.IsNullOrEmpty(path))
            return false;

        string[] parts = path.Split('.');
        object current = script;

        for (int i = 0; i < parts.Length; i++)
        {
            if (current == null)
                return false;

            string part = parts[i];

            Type type = current.GetType();

            // -----------------------------
            // 2. Field
            // -----------------------------
            FieldInfo field = type.GetField(part,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (field != null)
            {
                if (i == parts.Length - 1)
                {
                    field.SetValue(current, value);
                    return true;
                }

                current = field.GetValue(current);
                continue;
            }

            // -----------------------------
            // 3. Property
            // -----------------------------
            PropertyInfo prop = type.GetProperty(part,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (prop != null)
            {
                if (i == parts.Length - 1)
                {
                    prop.SetValue(current, value);
                    return true;
                }

                current = prop.GetValue(current);
                continue;
            }

            return false;
        }

        return false;
    }
}