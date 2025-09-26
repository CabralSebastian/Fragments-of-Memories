using UnityEngine;
using System;

public class StateMachineConfig : MonoBehaviour
{
  [SerializeField] private StateTypeReference _initialStateType;

  private IState _initialState;

  private void Awake()
  {
    if (_initialStateType.Type != null)
      _initialState = (IState)Activator.CreateInstance(_initialStateType.Type);
  }
}
