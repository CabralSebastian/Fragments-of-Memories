using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(StateTypeReference))]
public class StateTypeReferenceDrawer : PropertyDrawer
{
  private List<Type> _availableTypes;
  private string[] _typeNames;
  private int _selectedIndex;

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    SerializedProperty typeNameProp = property.FindPropertyRelative("_typeName");

    if (_availableTypes == null)
    {
      _availableTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(asm => asm.GetTypes())
        .Where(t => typeof(IState).IsAssignableFrom(t) && !t.IsAbstract)
        .ToList();

      _typeNames = _availableTypes.Select(t => t.Name).ToArray();
    }

    // Current selected index
    Type currentType = Type.GetType(typeNameProp.stringValue);
    _selectedIndex = Mathf.Max(0, _availableTypes.FindIndex(t => t == currentType));

    EditorGUI.BeginProperty(position, label, property);

    _selectedIndex = EditorGUI.Popup(position, label.text, _selectedIndex, _typeNames);

    typeNameProp.stringValue = _availableTypes[_selectedIndex].AssemblyQualifiedName;

    EditorGUI.EndProperty();
  }
}
