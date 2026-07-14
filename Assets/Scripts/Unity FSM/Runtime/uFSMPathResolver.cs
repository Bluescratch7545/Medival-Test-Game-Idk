using System;
using System.Reflection;

public static class uFSMPathResolver
{
    public static object GetValue(object target, string path)
    {
        if (target == null || string.IsNullOrEmpty(path))
            return null;

        string[] parts = path.Split('.');

        object current = target;

        foreach (string part in parts)
        {
            if (current == null)
                return null;

            Type type = current.GetType();

            FieldInfo field = type.GetField(part,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
            {
                current = field.GetValue(current);
                continue;
            }

            PropertyInfo prop = type.GetProperty(part,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (prop != null)
            {
                current = prop.GetValue(current);
                continue;
            }

            return null;
        }

        return current;
    }
}