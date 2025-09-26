using System;
using UnityEngine;

[Serializable]
public class StateTypeReference
{
  [SerializeField] private string _typeName;

  public Type Type => string.IsNullOrEmpty(_typeName) ? null : Type.GetType(_typeName);

  public void SetType(Type type)
  {
    if (typeof(IState).IsAssignableFrom(type))
      _typeName = type.AssemblyQualifiedName;
  }
}
