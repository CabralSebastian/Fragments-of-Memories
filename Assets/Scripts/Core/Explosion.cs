using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
  [SerializeField] private GameObject _explosionEffect;
  private float _radius;
  [SerializeField] private float _duration = 0.3f;
  private SphereCollider _collider;

  private readonly List<GameObject> _overlaps = new();

  public void Awake()
  {
    _collider = GetComponent<SphereCollider>();
    _collider.isTrigger = true;
    _radius = _collider.radius;
  }

  public void StartExplotion()
  {
    transform.SetParent(null);
    StartCoroutine(ExplodeCoroutine());
  }

  private IEnumerator ExplodeCoroutine()
  {
    float timer = 0f;

    while (timer < _duration)
    {
      float t = timer / _duration;
      _explosionEffect.transform.localScale = Mathf.Lerp(0f, _radius, t) * 2f * Vector3.one;
      timer += Time.deltaTime;
      yield return null;
    }

    yield return null;

    DamageOverlaps();
    _explosionEffect.SetActive(false);
    Destroy(gameObject);
  }

  private void DamageOverlaps()
  {
    foreach (GameObject overlap in _overlaps)
      if (overlap.TryGetComponent(out Ivy ivy))
        ivy.gameObject.SetActive(false);
  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("Colisiono con " + other.name);
    if (other.gameObject && !_overlaps.Contains(other.gameObject))
      _overlaps.Add(other.gameObject);
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject && _overlaps.Contains(other.gameObject))
      _overlaps.Remove(other.gameObject);
  }
}
