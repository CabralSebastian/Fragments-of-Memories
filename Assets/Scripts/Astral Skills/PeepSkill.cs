
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PeepSkill : MonoBehaviour
{
  [SerializeField] private float _activationTime = 0.5f;
  [SerializeField][Range(0, 1)] private float _isActive = 1;
  [SerializeField] private float _radius = 2;
  [SerializeField] private float _activeRadius = 0;

  public Vector3 Center => transform.position;
  public Vector3 Direction => transform.forward;
  public float IsActive => _isActive;
  public float Radius => _activeRadius;

  public void Activate()
  {
    StartCoroutine(ActivateCoroutine());
  }

  public void DeActivate()
  {
    StartCoroutine(DeActivateCoroutine());
  }

  private IEnumerator ActivateCoroutine()
  {
    while (_activeRadius < _radius)
    {
      _activeRadius += Time.deltaTime * _radius / _activationTime;
      _activeRadius = Math.Min(_activeRadius, _radius);

      yield return null;
    }
  }

  private IEnumerator DeActivateCoroutine()
  {
    while (_activeRadius > 0)
    {
      _activeRadius -= Time.deltaTime * _radius / _activationTime;
      _activeRadius = Math.Max(_activeRadius, 0);

      yield return null;
    }
  }
}
