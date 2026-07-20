using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IData), true)]
public class IDataPropertyDrawer : PropertyDrawer
{
    private static Type[] _implementingTypes;

    private static Type[] ImplementingTypes
    {
        get
        {
            if (_implementingTypes == null)
            {
                _implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(SafeGetTypes)
                    .Where(t => typeof(IData).IsAssignableFrom(t)
                                && !t.IsInterface
                                && !t.IsAbstract
                                && !t.IsGenericTypeDefinition) // open generics (REF<T>) can't be instantiated directly
                    .ToArray();
            }
            return _implementingTypes;
        }
    }

    private static Type[] SafeGetTypes(Assembly asm)
    {
        try { return asm.GetTypes(); }
        catch (ReflectionTypeLoadException e) { return e.Types.Where(t => t != null).ToArray(); }
    }

    private const float DropdownWidth = 90f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ManagedReference)
        {
            EditorGUI.LabelField(position, label.text, "Use [SerializeReference] with this drawer");
            return;
        }

        EditorGUI.BeginProperty(position, label, property);

        string currentTypeName = GetShortTypeName(property.managedReferenceFullTypename);
        bool hasType = !string.IsNullOrEmpty(property.managedReferenceFullTypename);

        // Foldout arrow + label share the row with the type dropdown, same as a normal list element.
        var foldoutRect = new Rect(position.x, position.y, position.width - DropdownWidth - 4, EditorGUIUtility.singleLineHeight);
        var dropdownRect = new Rect(position.xMax - DropdownWidth, position.y, DropdownWidth, EditorGUIUtility.singleLineHeight);

        if (hasType)
        {
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);
        }
        else
        {
            EditorGUI.LabelField(foldoutRect, label);
        }

        if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(currentTypeName), FocusType.Keyboard))
        {
            var menu = new GenericMenu();
            if (property.name == null || property.name == "<null>")
            {
                menu.AddItem(new GUIContent("<null>"), !hasType, () =>
                {
                    property.managedReferenceValue = null;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            foreach (var type in ImplementingTypes)
            {
                var capturedType = type;
                menu.AddItem(new GUIContent(type.Name), type.Name == currentTypeName, () =>
                {
                    property.managedReferenceValue = Activator.CreateInstance(capturedType);
                    property.isExpanded = true;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.DropDown(dropdownRect);
        }

        // Draw the concrete type's fields only when expanded, indented underneath.
        if (hasType && property.isExpanded)
        {
            EditorGUI.indentLevel++;
            float y = position.y + EditorGUIUtility.singleLineHeight + 2;

            var iterator = property.Copy();
            var end = iterator.GetEndProperty();
            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
            {
                enterChildren = false;
                float h = EditorGUI.GetPropertyHeight(iterator, true);
                var childRect = new Rect(position.x, y, position.width, h);
                EditorGUI.PropertyField(childRect, iterator, true);
                y += h + 2;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + 2;

        bool hasType = property.propertyType == SerializedPropertyType.ManagedReference
                       && !string.IsNullOrEmpty(property.managedReferenceFullTypename);

        if (hasType && property.isExpanded)
        {
            var iterator = property.Copy();
            var end = iterator.GetEndProperty();
            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
            {
                enterChildren = false;
                height += EditorGUI.GetPropertyHeight(iterator, true) + 2;
            }
        }

        return height;
    }

    private static string GetShortTypeName(string managedReferenceFullTypename)
    {
        if (string.IsNullOrEmpty(managedReferenceFullTypename)) return "<null>";
        // Format is "AssemblyName TypeFullName" — grab the type part, then the last segment after '.'
        int spaceIndex = managedReferenceFullTypename.IndexOf(' ');
        string typeFullName = spaceIndex >= 0 ? managedReferenceFullTypename.Substring(spaceIndex + 1) : managedReferenceFullTypename;
        int dotIndex = typeFullName.LastIndexOf('.');
        return dotIndex >= 0 ? typeFullName.Substring(dotIndex + 1) : typeFullName;
    }
}